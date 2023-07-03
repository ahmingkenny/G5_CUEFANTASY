using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    [SerializeField] public bool isControlling = false;
    private bool isSelected = false;
    private bool isAdjusting = false;
    private bool isAdjusted = false;
    private bool isFollowed = false;
    private bool isMicroAdjusting = false;
    private bool isMicroAdjusted = false;
    private bool isShooting = false;
    private bool isCalled = false;
    private Vector3 AIBallSpawnPos = new Vector3(0, 0, 0f);

    private GameObject closestBall;

    [Header("Game Reference")]
    private GameObject MainCamera;
    private PerspectiveView perspectiveView;
    private GameFlow gameFlow;
    private TopView topView;
    private CueBallFollower cueBallFollower;
    private Slider powerSlider;
    private BallShooter ballShooter;
    private GameObject Cue;

    [Header("UI Reference")]
    private GameObject AITurnCover;
    private GameObject Text;



    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        perspectiveView = MainCamera.GetComponent<PerspectiveView>();
        gameFlow = this.gameObject.GetComponent<GameFlow>();
        topView = MainCamera.GetComponent<TopView>();
        cueBallFollower = MainCamera.GetComponent<CueBallFollower>();
        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<Slider>();
        ballShooter = MainCamera.GetComponent<BallShooter>();
        AITurnCover = GameObject.Find("AITurnCover");
        Text = AITurnCover.transform.Find("Text").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlling)
        {
            if (isSelected == false)
            {
                topView.ChoosePosition(AIBallSpawnPos);
                GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");

                if (CueBall.GetComponent<Rigidbody>().velocity.z == 0 && CueBall.GetComponent<Rigidbody>().velocity.y == 0)
                {
                    if (!isCalled)
                    {
                        Invoke("isAdjustingSwitch", Random.Range(1.5f, 2.5f));
                        isCalled = true;
                    }
                    isSelected = true;
                }
            }

            if (isAdjusting && isSelected)
            {
                AdjustCameraAngle();
                if (!isCalled)
                {
                    Invoke("isAdjustedSwitch", Random.Range(2.5f, 3f));
                    isCalled = true;
                }
            }

            if (isAdjusted && !isFollowed)
            {
                cueBallFollower.SwitchFollowing();
                if (!isCalled)
                {
                    Invoke("isMicroAdjustingSwitch", Random.Range(1f, 1.5f));
                    isCalled = true;
                }
                isFollowed = true;
            }

            if (isMicroAdjusting && isFollowed)
            {
                MicroAdjustment();
                if (!isCalled)
                {
                    Invoke("isShootingSwitch", Random.Range(0.1f, 0.5f));
                    isCalled = true;
                }
            }

            if (isShooting && isMicroAdjusted)
            {
                Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().Charging();
                powerSlider.value += 0.005f;
                if (!isCalled)
                {
                    Invoke("Shoot", Random.Range(1f, 2.5f));
                    isCalled = true;
                }
            }

        }

    }

    public void ActiveAIController()
    {
        isControlling = true;
        AITurnCover.GetComponent<Image>().enabled = true;
        Text.GetComponent<Text>().enabled = true;

    }

    private void FindClosestBall()
    {
        GameObject cueBall = GameObject.FindGameObjectWithTag("CueBall");
        GameObject jadeBall = GameObject.FindGameObjectWithTag("JadeBall");
        closestBall = jadeBall;
        float closestDist = Vector3.Distance(jadeBall.transform.position, cueBall.transform.position);

        GameObject[] balls;
        if(GameFlow.turn == GameFlow.Turn.Defender)
        {
            balls = GameObject.FindGameObjectsWithTag("DefenderBall");
        }
        else
        {
            balls = GameObject.FindGameObjectsWithTag("AttackerBall");
        }

        if (balls != null)
        {
            closestBall = balls[0];
            closestDist = Vector3.Distance(balls[0].transform.position, cueBall.transform.position);

            foreach (GameObject ball in balls)
            {

                if (Vector3.Distance(ball.transform.position, cueBall.transform.position) < closestDist)
                {
                    closestBall = ball;
                    closestDist = Vector3.Distance(ball.transform.position, cueBall.transform.position);
                }
            }
        }
    }

    private void isMicroAdjustingSwitch()
    {
        isCalled = false;
        isMicroAdjusting = true;
    }


    private void isAdjustingSwitch()
    {
        isCalled = false;
        isAdjusting = false;
        isAdjusting = true;
    }
    private void isAdjustedSwitch()
    {
        isCalled = false;
        isAdjusted = true;
        isAdjusting = false;
    }

    private void isShootingSwitch()
    {
        isCalled = false;
        isMicroAdjusting = false;
        isMicroAdjusted = true;
        isShooting = true;
    }

    private void Shoot()
    {
        Cue = GameObject.FindGameObjectWithTag("Cue");
        Cue.GetComponent<CueBehaviour>().Swing();
        ballShooter.Shoot();
        Reset();
    }

    private void AdjustCameraAngle()
    {
        FindClosestBall();
        Vector3 CamPos = MainCamera.transform.forward;
        CamPos.y = 0;
        Vector3 BallPos = closestBall.transform.position;
        BallPos.y = 0;
        Vector3 CamPos2 = MainCamera.transform.position;
        CamPos2.y = 0;
        float angle = Vector3.Angle(CamPos, BallPos - CamPos2);
           
        Vector3 cameraRelative = MainCamera.transform.InverseTransformPoint(closestBall.transform.position);

        if (angle > 0.75f && cameraRelative.x < 0)
        {
            perspectiveView.TurnLeft();
        }
        else if (angle > 0.75f && cameraRelative.x > 0)
        {
            perspectiveView.TurnRight();
        }

    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }

    private void MicroAdjustment()
    {
        GameObject[] territories;
        territories = FindGameObjectsInLayer(11);
        float closestDist = 100f;
        GameObject closestTerritory = territories[0];
        if (GameFlow.turn == GameFlow.Turn.Defender)
        {

            foreach (GameObject territory in territories)
            {
                GameObject targetTerritory = territory;
                TerritoryBehaviour targetBehaviour = targetTerritory.GetComponent<TerritoryBehaviour>();

                if (targetBehaviour.faction == TerritoryBehaviour.Faction.Attacker && Vector3.Distance(targetTerritory.transform.position, closestBall.transform.position) < closestDist)
                {
                    closestTerritory = territory;
                    closestDist = Vector3.Distance(territory.transform.position, closestBall.transform.position);
                }
            }

        }
        else
        {
            int i = 0;
            GameObject targetTerritories = territories[i];
            TerritoryBehaviour territoryBehaviour = targetTerritories.GetComponent<TerritoryBehaviour>();
            closestDist = Vector3.Distance(targetTerritories.transform.position, closestBall.transform.position);

            foreach (GameObject territory in territories)
            {
                GameObject targetTerritory = territory;
                TerritoryBehaviour targetBehaviour = targetTerritory.GetComponent<TerritoryBehaviour>();

                if (targetBehaviour.faction == TerritoryBehaviour.Faction.Defender && Vector3.Distance(targetTerritory.transform.position, closestBall.transform.position) < closestDist)
                {
                    closestTerritory = territory;
                    closestDist = Vector3.Distance(territory.transform.position, closestBall.transform.position);
                }
            }

        }

        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        Vector3 ballRelative = CueBall.transform.InverseTransformPoint(closestTerritory.transform.position);

        if (ballRelative.x < 0)
        {
            cueBallFollower.MicroTurnRight();
        }
        else if (ballRelative.x > 0)
        {
            cueBallFollower.MicroTurnLeft();
        }

    }

    public void Reset()
    {
        isSelected = false;
        isAdjusting = false;
        isAdjusted = false;
        isFollowed = false;
        isMicroAdjusting = false;
        isMicroAdjusted = false;
        isShooting = false;
        isControlling = false;
        isCalled = false;
        AITurnCover.GetComponent<Image>().enabled = false;
        Text.GetComponent<Text>().enabled = false;
    }

}
