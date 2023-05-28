using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockColumn : MonoBehaviour, IDestroyable, IDamageable
{
    [SerializeField] private GameObject SmallDustExplosionFX;
    [SerializeField] private GameObject DustExplosionFX;
    [SerializeField] private int hp = 20;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float speed = 0.5f;

    private GameObject GameManager;
    private GameFlow gameFlow;
    private int startTurn;

    [Header("Audio")]
    [SerializeField] private AudioClip RockDestroySound;

    void Awake()
    {
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        startTurn = gameFlow.turnNum;
        targetPosition = this.transform.position;
        startPosition.x = targetPosition.x;
        startPosition.z = targetPosition.z;
        startPosition.y = targetPosition.y - 0.5f;
        this.transform.position = startPosition;
    }

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);

        if (this.transform.position != targetPosition)
        {
            Instantiate(SmallDustExplosionFX, this.transform.position, Quaternion.identity);
        }

        if (gameFlow.turnNum != startTurn)
        {
            startTurn++;
            Hit(10);
        }

        if (hp <= 0)
        {
            DestroyIt();
        }
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
        AudioSource.PlayClipAtPoint(RockDestroySound, this.transform.position);
        Instantiate(DustExplosionFX, this.transform.position, Quaternion.identity);
    }

    public void Hit(int damage)
    {
        hp -= damage;
    }

}
