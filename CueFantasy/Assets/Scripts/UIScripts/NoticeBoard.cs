using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeBoard : MonoBehaviour
{
    private GameObject Notice;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        Notice = GameObject.Find("Notice");
        text = Notice.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAttackerTurn()
    {
        text.text = "進攻方回合";
    }

    public void ShowDefenderTurn()
    {
        text.text = "防守方回合";
    }

    public void ShowAttackerFoul()
    {
        text.text = "進攻方違反律例\n請防守方指定白玉位置";
    }

    public void ShowDefenderFoul()
    {
        text.text = "防守方違反律例\n請進攻方指定白玉位置";
    }

    public void ShowCastingAbility()
    {
        text.text = "正在準備施放技能\n按左鍵選擇施放位置\n(按右鍵取消)";
    }

    public void ShowInsufficientMana()
    {
        text.text = "能量不足\n無法施放技能";
    }

    public void ShowCancelCasting()
    {
        text.text = "取消施放技能";
    }

    public void ShowSuccessCasting()
    {
        text.text = "技能施放成功 !";
    }

    public void ShowAdvisorType()
    {
        text.text = "策士";
    }

    public void ShowSoldierType()
    {
        text.text = "步兵";
    }

    public void ShowArcherType()
    {
        text.text = "弓兵";
    }
    public void ShowHorseType()
    {
        text.text = "騎兵";
    }
    public void ShowJadeType()
    {
        text.text = "玉璽";
    }


}
