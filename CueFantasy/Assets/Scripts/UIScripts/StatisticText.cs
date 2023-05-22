using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticText : MonoBehaviour
{
    private Text text;
    private GameObject GameManager;
    private Attacker attacker;
    private Defender defender;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
        UpDateText();
    }

    private void UpDateText()
    {
        text = GetComponent<Text>();
        text.text = "[進攻方]\n攻破敵陣 - " + attacker.ballIn + "\n合法碰撞 - " + attacker.legalHit + "\n非法碰撞 - " + attacker.illegalHit + "\n違反律例 - " + attacker.foul +
                    "\n\n[防守方]\n攻破敵陣 - " + defender.ballIn + "\n合法碰撞 - " + defender.legalHit + "\n非法碰撞 - " + defender.illegalHit + "\n違反律例 - " + defender.foul;
    }

}
