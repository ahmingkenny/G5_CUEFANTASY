using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReporter : MonoBehaviour
{
    private GameObject Report;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        Report = GameObject.Find("Report");
        text = Report.GetComponent<Text>();
    }

    public void ShowRainStorm()
    {
        text.text += "\n狂風暴雨正在肆虐 !";
    }

    public void ShowSnowStorm()
    {
        text.text += "\n正在吹起漫天風雪 !";
    }

    public void ShowThirdPartyInvasion()
    {
        text.text += "\n地方勢力正在崛起 !";
    }
    public void ShowRainStormStopped()
    {
        text.text += "\n天氣已回復晴朗";
    }
    public void ShowSnowStormStopped()
    {
        text.text += "\n天氣已回復晴朗";
    }
    public void ShowThirdPartyInvasionStopped()
    {
        text.text += "\n地方勢力已經敗退";
    }

    public void ShowAttackerIn()
    {
        text.text += "\n進攻方攻破敵陣 !";
    }

    public void ShowDefenderIn()
    {
        text.text += "\n防守方攻破敵陣 !";
    }

    public void clearText()
    {
        text.text = "戰報:";
    }

}
