using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEventBehaviour : MonoBehaviour
{
    public bool isEventing = false;
    private int eventLife = 3;
    private int currentEventLife; 
    private int startTurn;
    private bool isRaining = false;
    private bool isSnowing = false;
    private bool isInvading = false;

    private GameObject GameManager;
    private GameFlow gameFlow;
    private GameObject BattleReporter;
    private BattleReporter battleReporter;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        BattleReporter = GameObject.Find("BattleReporter");
        battleReporter = BattleReporter.GetComponent<BattleReporter>();
        startTurn = gameFlow.turnNum;
    }

    void Update()
    {
        if (isEventing)
        {
            if (gameFlow.turnNum != startTurn)
            {
                startTurn++;
                currentEventLife--;
            }

            if (currentEventLife == 0)
            {
                if (isRaining)
                {
                    isRaining = false;
                    battleReporter.ShowRainStormStopped();
                }
                else if (isSnowing)
                {
                    isSnowing = false;
                    battleReporter.ShowSnowStormStopped();
                }
                else if (isInvading)
                {
                    isInvading = false;
                    battleReporter.ShowThirdPartyInvasionStopped();
                }

                isEventing = false;

                currentEventLife = eventLife;
            }
        }
    }

    public void CreateRainStorm()
    {
        startTurn = gameFlow.turnNum;
        currentEventLife = eventLife;
        isEventing = true;
        isRaining = true;
        battleReporter.ShowRainStorm();
    }

    public void CreateSnowStorm()
    {
        startTurn = gameFlow.turnNum;
        currentEventLife = eventLife;
        isEventing = true;
        isSnowing = true;
        battleReporter.ShowSnowStorm();
    }

    public void CreateThirdPartyInvasion()
    {
        startTurn = gameFlow.turnNum;
        currentEventLife = eventLife;
        isEventing = true;
        isInvading = true;
        battleReporter.ShowThirdPartyInvasion();
    }

    public void ShowEvent()
    {
        if (isRaining)
        {
            battleReporter.ShowRainStorm();
        }
        else if (isSnowing)
        {
            battleReporter.ShowSnowStorm();
        }
        else if (isInvading)
        {
            battleReporter.ShowThirdPartyInvasion();
        }
    }

}
