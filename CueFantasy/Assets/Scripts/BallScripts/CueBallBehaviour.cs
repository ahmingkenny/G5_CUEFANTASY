using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallBehaviour : MonoBehaviour
{
    [SerializeField] private float reflectRatio = 0.2f;
    [SerializeField] private int damage = 5;

    public static int legalHit = 0;
    public static int illegalHit = 0;

    [Header("GameReference")]
    private GameObject MainCamera;
    private BallShooter ballShooter;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private PerspectiveView perspectiveView;
    private TopView topView;
    private Attacker attacker;
    private Defender defender;

    [Header("Audio")]
    [SerializeField] private AudioClip ImpactSound;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ballShooter = MainCamera.GetComponent<BallShooter>();
        perspectiveView = MainCamera.GetComponent<PerspectiveView>();
        topView = MainCamera.GetComponent<TopView>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = this.transform.position - collision.transform.position;

        if (BallShooter.isTopSpin == true)
        {
            if (collision.collider.tag == "DefenderBall" || collision.collider.tag == "AttackerBall")
            {
                GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * reflectRatio/10 + GetComponent<Rigidbody>().velocity * reflectRatio/10, collision.contacts[0].point, ForceMode.Impulse);
            }
        }
        else if (BallShooter.isBackSpin == true)
        {
            if (collision.collider.tag == "DefenderBall" || collision.collider.tag == "AttackerBall")
            {
                GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * reflectRatio + GetComponent<Rigidbody>().velocity * -reflectRatio, collision.contacts[0].point, ForceMode.Impulse);
            }
        }
        else if (BallShooter.isLeftSpin == true)
        {
            if (collision.collider.tag == "DefenderBall" || collision.collider.tag == "AttackerBall")
            {
                Vector3 velocity = GetComponent<Rigidbody>().velocity;
                velocity.x = velocity.x * Mathf.Cos(45f) - velocity.z * Mathf.Sin(45f);
                velocity.z = velocity.x * Mathf.Sin(45f) + velocity.z * Mathf.Cos(45f);
                GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * reflectRatio + velocity * reflectRatio/3, collision.contacts[0].point, ForceMode.Impulse);
            }
        }
        else if (BallShooter.isRightSpin == true)
        {
            if (collision.collider.tag == "DefenderBall" || collision.collider.tag == "AttackerBall")
            {
                Vector3 velocity = GetComponent<Rigidbody>().velocity;
                velocity.x = velocity.x * Mathf.Cos(-45f) - velocity.z * Mathf.Sin(-45f);
                velocity.z = velocity.x * Mathf.Sin(-45f) + velocity.z * Mathf.Cos(-45f);
                GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * reflectRatio + velocity * reflectRatio/3, collision.contacts[0].point, ForceMode.Impulse);
            }
        }

        if (GameFlow.turn == GameFlow.Turn.Attacker)
        {
            if (collision.collider.tag == "AttackerBall" || collision.collider.tag == "JadeBall")
            {
                attacker.legalHit++;
                legalHit++;
            }
            else if (collision.collider.tag == "DefenderBall")
            {
                attacker.illegalHit++;
                illegalHit++;
            }
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender)
        {
            if (collision.collider.tag == "DefenderBall" || collision.collider.tag == "JadeBall")
            {
                defender.legalHit++;
                legalHit++;
            }
            else if (collision.collider.tag == "AttackerBall")
            {
                defender.illegalHit++;
                illegalHit++;
            }
        }

        if (collision.collider.tag == "Destructible")
        {
            collision.gameObject.GetComponent<IDamageable>().Hit(damage);
            AudioSource.PlayClipAtPoint(ImpactSound, this.transform.position);
        }

        if (collision.collider.tag == "Terrain")
        {
            AudioSource.PlayClipAtPoint(ImpactSound, this.transform.position);
        }

    }

    public void InitializeData()
    {
        legalHit = 0;
        illegalHit = 0;
    }

    public void SetBallPosition(Vector3 targetPosition)
    {
        Vector3 spawnPosition = new Vector3(targetPosition.x, targetPosition.y +1f, targetPosition.z);
        this.transform.position = spawnPosition;
        PerspectiveView.isSelectLerp = true;
        TopView.isSelecting = false;
    }

    public void Reset()
    {
        legalHit = 0;
        illegalHit = 0;
    }

}
