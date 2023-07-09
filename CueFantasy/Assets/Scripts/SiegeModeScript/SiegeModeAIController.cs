using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeModeAIController : MonoBehaviour
{
    [SerializeField] private bool isChargeReady = true;
    [SerializeField] private float chargeInterval = 3f;

    [SerializeField] public bool isPhase2 = false;
    [SerializeField] private GameObject Meteor;
    [SerializeField] private GameObject MeteorRangeIndicator;
    private GameObject indicator;
    [SerializeField] private bool isMeteorOnCoolDown = false;
    [SerializeField] private float meteorTravelTime = 6f;
    [SerializeField] private float meteorInterval = 3f;
    [SerializeField] private float meteorForce = 3f;
    private Vector3 meteorStartPosition;
    private Vector3 targetPosition;
    private Vector3 targetDirection;
    private GameObject GameManager;
    private GameFlow gameFlow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        meteorStartPosition = GameObject.Find("Yurt").transform.position;
        meteorStartPosition.y += 2.5f;

    }

    // Update is called once per frame
    void Update()
    {

        if (gameFlow.weaponIsSelected)
        {

            if (isChargeReady)
            {
                Invoke("CallCharge", chargeInterval);
                isChargeReady = false;
            }

        }

        if (isPhase2)
        {
            if (!isMeteorOnCoolDown)
            {
                Invoke("CallMeteor", meteorTravelTime);
                targetPosition = GameObject.FindGameObjectWithTag("JadeBall").transform.position;
                targetPosition.y = 0f;
                targetDirection = (GameObject.FindGameObjectWithTag("JadeBall").transform.position - meteorStartPosition).normalized;
                Vector3 indicatorPosition = targetPosition;
                indicatorPosition.y -= 0.615f;
                indicator = Instantiate(MeteorRangeIndicator, indicatorPosition, Quaternion.identity);
                isMeteorOnCoolDown = true;
            }
        }

    }

    private void CallCharge()
    {
        isChargeReady = true;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("DefenderBall");
        GameObject initiator = balls[Random.Range(0, balls.Length - 1)];
        Vector3 targetDirection = (GameObject.FindGameObjectWithTag("JadeBall").transform.position - initiator.transform.position).normalized;
        initiator.GetComponent<BossTroopsBehaviour>().Charge(targetDirection);
    }

    private void CallMeteor()
    {
        Invoke("CoolDownDone", meteorInterval);
        Destroy(indicator);
        GameObject meteor = Instantiate(Meteor, meteorStartPosition, Quaternion.identity);
        meteor.GetComponent<Rigidbody>().AddForce(targetDirection * meteorForce, ForceMode.Impulse);
    }
    private void CoolDownDone()
    {
        isMeteorOnCoolDown = false;
    }

    public void StartPhase2()
    {
        isPhase2 = true;
    }

}
