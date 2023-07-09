using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMining : MonoBehaviour
{
    private GameObject GameManager;
    private GameFlow gameFlow;
    private Attacker attacker;
    private Defender defender;
    private GameObject NoticeBoard;
    private NoticeBoard noticeBoard;
    [SerializeField] private AudioClip MiningSound;
    [SerializeField] private GameObject MiningFX;
    [SerializeField] private int resourceAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        NoticeBoard = GameObject.Find("NoticeBoard");
        noticeBoard = NoticeBoard.GetComponent<NoticeBoard>();
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "CueBall")
        {
            Instantiate(MiningFX, collision.contacts[0].point, Quaternion.identity);
            AudioSource.PlayClipAtPoint(MiningSound, this.transform.position);
            noticeBoard.ShowSuccessMining();
            if (GameFlow.turn == GameFlow.Turn.Attacker)
            {
                attacker.GainResource(resourceAmount);
            }
            else if (GameFlow.turn == GameFlow.Turn.Defender)
            {
                defender.GainResource(resourceAmount);
            }

        }
    }
}
