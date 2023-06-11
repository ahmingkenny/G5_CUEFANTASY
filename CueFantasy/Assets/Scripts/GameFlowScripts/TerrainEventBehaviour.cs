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

    [SerializeField] private GameObject RainParticle;
    [SerializeField] private PhysicMaterial OriginalPhysicMat;
    [SerializeField] private PhysicMaterial RainPhysicMat;
    private GameObject Rain;

    [SerializeField] private GameObject SnowObject;
    [SerializeField] private GameObject SnowParticle;
    private GameObject SnowFall;
    private GameObject Snow;

    [SerializeField] private GameObject NeutralTerritoryObject;
    private GameObject NeutralTerritory;


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
                    Destroy(Rain, 0);
                    GetComponent<Collider>().material = OriginalPhysicMat;
                }
                else if (isSnowing)
                {
                    isSnowing = false;
                    battleReporter.ShowSnowStormStopped();
                    Destroy(Snow, 0);
                    Destroy(SnowFall, 0);
                }
                else if (isInvading)
                {
                    isInvading = false;
                    battleReporter.ShowThirdPartyInvasionStopped();
                    Destroy(NeutralTerritory, 0);
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
        Rain = Instantiate(RainParticle, this.transform.position, Quaternion.identity);
        Rain.transform.parent = gameObject.transform;
        GetComponent<Collider>().material = RainPhysicMat;
    }

    public void CreateSnowStorm()
    {
        startTurn = gameFlow.turnNum;
        currentEventLife = eventLife;
        isEventing = true;
        isSnowing = true;
        battleReporter.ShowSnowStorm();
        Snow = Instantiate(SnowObject, this.transform.position, Quaternion.identity);
        Snow.transform.parent = gameObject.transform;
        SnowFall = Instantiate(SnowParticle, this.transform.position, Quaternion.identity);
        SnowFall.transform.parent = gameObject.transform;
    }

    public void CreateThirdPartyInvasion()
    {
        startTurn = gameFlow.turnNum;
        currentEventLife = eventLife;
        isEventing = true;
        isInvading = true;
        battleReporter.ShowThirdPartyInvasion();
        NeutralTerritory = Instantiate(NeutralTerritoryObject, this.transform.position, Quaternion.identity);
        NeutralTerritory.transform.parent = gameObject.transform;
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
