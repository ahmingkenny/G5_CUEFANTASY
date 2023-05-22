using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJail : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject SplashFX;
    [SerializeField] private GameObject smallSplashFX;
    [SerializeField] private int hp = 40;

    private GameObject GameManager;
    private GameFlow gameFlow;
    private int startTurn;

    [Header("Audio")]
    [SerializeField] private AudioClip WaterSound;
    [SerializeField] private AudioClip SplashSound;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        startTurn = gameFlow.turnNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFlow.turnNum != startTurn)
        {
            startTurn++;
            TakeDamage(10);
        }

        if (hp <= 0)
        {
            MoveUp();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Instantiate(smallSplashFX, other.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(SplashSound, this.transform.position);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
        {
            Rigidbody ballRigid = other.gameObject.GetComponent<Rigidbody>();
            ballRigid.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
        {
            Rigidbody ballRigid = other.gameObject.GetComponent<Rigidbody>();
            ballRigid.constraints = RigidbodyConstraints.None;
        }
    }

    private void MoveUp()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime);
        Invoke("DestroyIt", 2f);
    }

    public void DestroyIt()
    {
        SphereCollider myCollider = GetComponent<SphereCollider>();
        myCollider.radius = 0;
        Destroy(this.gameObject);
        Instantiate(SplashFX, this.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(WaterSound, this.transform.position);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

}
