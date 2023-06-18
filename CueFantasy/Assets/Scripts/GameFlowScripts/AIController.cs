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
    private bool isShooting = false;
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
                Invoke("isAdjustingSwitch", 3f);
                isSelected = true;
            }

            if (isAdjusting && !isAdjusted)
            {
                AdjustCameraAngle();
                Invoke("isAdjustedSwitch", 3f);
            }

            if (isAdjusted && !isFollowed)
            {
                cueBallFollower.SwitchFollowing();
                isFollowed = true;
                Invoke("isShootingSwitch", 1.5f);
            }

            if (isShooting)
            {
                Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().Charging();
                powerSlider.value += 0.005f;
                Invoke("Shoot", 2f);
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

    private void isAdjustingSwitch()
    {
        isAdjusting = true;
    }
    private void isAdjustedSwitch()
    {
        isAdjusting = false;
        isAdjusted = true;
    }

    private void isShootingSwitch()
    {
        isShooting = true;
    }

    private void Shoot()
    {
        ballShooter.Shoot();
        Cue = GameObject.FindGameObjectWithTag("Cue");
        Cue.GetComponent<CueBehaviour>().Swing();
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

        if (angle > 1f && cameraRelative.x < 0)
        {
            perspectiveView.TurnLeft();
        }
        else if (angle > 1f && cameraRelative.x > 0)
        {
            perspectiveView.TurnRight();
        }

    }

    public void Reset()
    {
        isSelected = false;
        isAdjusting = false;
        isAdjusted = false;
        isFollowed = false;
        isShooting = false;
        isControlling = false;
        AITurnCover.GetComponent<Image>().enabled = false;
        Text.GetComponent<Text>().enabled = false;
    }

}
