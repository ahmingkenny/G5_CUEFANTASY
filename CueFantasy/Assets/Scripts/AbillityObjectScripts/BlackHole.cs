using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour, IDestroyable, IDamageable
{
    [SerializeField] private GameObject SmokeFX;
    [SerializeField] private int hp = 30;
    [SerializeField] private float floatHeight = 0.1f;
    [SerializeField] private float drawSpeed = 0.05f;
    [SerializeField] private float height = 0.4f;
    [SerializeField] private float massMultiplier = 10f;

    private Vector3 finalPosition;
    private bool isContracting = false;
    private Vector3 contractRatio = new Vector3(-0.0008f, -0.0008f, -0.0008f);

    private GameObject GameManager;
    private GameFlow gameFlow;
    private int startTurn;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        startTurn = gameFlow.turnNum;
        SmokeFX = Instantiate(SmokeFX, this.transform.position, Quaternion.identity);
        SmokeFX.transform.parent = gameObject.transform;

        finalPosition.x = this.transform.position.x;
        finalPosition.z = this.transform.position.z;
        finalPosition.y = this.transform.position.y + height;
        this.transform.position = finalPosition;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
        {
            Rigidbody ballRigid = other.gameObject.GetComponent<Rigidbody>();
            ballRigid.velocity = Vector3.zero;
            ballRigid.mass *= massMultiplier;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
        {
            Rigidbody ballRigid = other.gameObject.GetComponent<Rigidbody>();
            ballRigid.useGravity = false;
            Vector3 targetPosition;
            targetPosition.x = other.transform.position.x;
            targetPosition.z = other.transform.position.z;
            targetPosition.y = other.transform.position.y + floatHeight;
            other.gameObject.transform.position = Vector3.MoveTowards(other.transform.position, targetPosition, drawSpeed * Time.deltaTime);
            other.gameObject.transform.position = Vector3.MoveTowards(other.transform.position, this.transform.position, drawSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
        {
            Rigidbody ballRigid = other.gameObject.GetComponent<Rigidbody>();
            ballRigid.useGravity = true;
            ballRigid.mass /= massMultiplier;
        }
    }


    void Update()
    {
        if (gameFlow.turnNum != startTurn)
        {
            startTurn++;
            Hit(10);
        }

        if (hp <= 0)
        {
            isContracting = true;
            MoveUp();
        }

        if (isContracting)
        {
            this.transform.localScale += contractRatio;
        }

    }

    public void Hit(int damage)
    {
        hp -= damage;
    }

    public void DestroyIt()
    {
        SphereCollider myCollider = GetComponent<SphereCollider>();
        myCollider.radius = 0;
        Destroy(this.gameObject);
    }

    private void MoveUp()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime);
        Invoke("DestroyIt", 2f);
    }

}
