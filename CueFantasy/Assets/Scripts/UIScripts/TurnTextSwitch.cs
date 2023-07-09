using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTextSwitch : MonoBehaviour
{
    [SerializeField] private int startYear = 1355;
    [SerializeField] private int startMonth = 6;

    private GameObject GameManager;
    private GameFlow gameFlow;
    private Text text;
    [SerializeField] private bool isSiegeMode;


    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnText()
    {
        startMonth++;
        if(startMonth > 12)
        {
            startMonth = 1;
            startYear++;
        }
        if (!isSiegeMode)
        {
            text.text = startYear + "年 " + startMonth + "月 " + Random.Range(1, 29) + "日\n剩餘回合: " + (gameFlow.turnLimit - gameFlow.turnNum);
        }
        else
        text.text = startYear + "年 " + startMonth + "月 " + Random.Range(1, 29) + "日";
    }

}
