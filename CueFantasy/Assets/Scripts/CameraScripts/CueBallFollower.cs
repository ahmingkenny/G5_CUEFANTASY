﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CueBallFollower : MonoBehaviour
{
    [SerializeField] private float camSpeed = 4f;
    [SerializeField] private float minCamDist = 0.6f;
    [SerializeField] private float lerpSpeed = 4f;
    [SerializeField] private float CamHeight = 0.3f;
    [SerializeField] private float minCamHeight = -0.2f;
    [SerializeField] private float maxCamHeight = 0.4f;
    [SerializeField] private float adjustSpeed = 0.1f;
    [SerializeField] private float superMicroMoveSpeed = 2.5f;
    [SerializeField] private PhysicMaterial OriginalBallPhysicMat;
    [SerializeField] private PhysicMaterial JumpBallPhysicMat;

    public static bool isFollowing = false;
    public static bool isJumpBallMode = false;

    private Slider powerSlider;
    private AIController aiController;
    private PerspectiveView perspectiveView;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private GameObject TransitionCamera;
    private TransitionCameraBehaviour transitionCameraBehaviour;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<Slider>();
        aiController = GameObject.Find("GameManager").GetComponent<AIController>();
        perspectiveView = GetComponent<PerspectiveView>();
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        if (gameFlow.isSiegeMode)
        {
            TransitionCamera = GameObject.Find("TransitionCamera");
            transitionCameraBehaviour = TransitionCamera.GetComponent<TransitionCameraBehaviour>();
        }

    }

    void Update()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");

        if (Input.GetKeyDown(KeyCode.Tab) && !TopView.isViewing && !TopView.isSelecting && !PerspectiveView.isSelectLerp && !PerspectiveView.isLerping && !BallShooter.isShooting && !BallShooter.isShoot && CueBall.GetComponent<Rigidbody>().velocity.z == 0 && !aiController.isControlling && gameFlow.weaponIsSelected)
        {
            GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

            SwitchFollowing();

        }

        if (!isJumpBallMode)
        {
            CueBall.GetComponent<Collider>().material = OriginalBallPhysicMat;
        }

        if (isFollowing)
        {

            if (!isJumpBallMode)
            {
                Vector3 lockedPosition = this.transform.position;
                lockedPosition.y = CamHeight;
                this.transform.position = Vector3.Lerp(this.transform.position, lockedPosition, lerpSpeed * Time.deltaTime);
            }

            powerSlider.interactable = true;

            this.transform.LookAt(CueBall.transform.position);

            if (Vector3.Distance(this.transform.position, CueBall.transform.position) != minCamDist) //keep the distance between cueball and camera at a range.
            {
                this.transform.position = Vector3.Lerp(this.transform.position, (this.transform.position - CueBall.transform.position).normalized * minCamDist + CueBall.transform.position, camSpeed* Time.deltaTime);
            }

            /*if (Input.GetKey(KeyCode.W) && !aiController.isControlling)
            {
                if (this.transform.position.y - CueBall.transform.position.y < maxCamHeight) //camera height control.
                {
                    this.transform.Translate(Vector3.up * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.S) && !aiController.isControlling)
            {
                if (this.transform.position.y - CueBall.transform.position.y > minCamHeight) //camerea height control.
                {
                    this.transform.Translate(Vector3.down * Time.deltaTime);
                }
            }*/
            if (!gameFlow.isSiegeMode)
            {
                if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift) && !aiController.isControlling)
                {
                    MicroTurnLeft();
                }

                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift) && !aiController.isControlling)
                {
                    MicroTurnRight();
                }

                if (Input.GetKeyUp(KeyCode.BackQuote) && !aiController.isControlling)
                {
                    SwitchToJumpBallMode();
                }

                if (isJumpBallMode)
                {
                    CueBall.GetComponent<Collider>().material = JumpBallPhysicMat;
                    MaintainJumpBallAngle();
                }

            }
            else if (gameFlow.isSiegeMode)
            {
                if (transitionCameraBehaviour.isTransited == true)
                {
                    if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift) && !aiController.isControlling)
                    {
                        MicroTurnLeft();
                    }

                    if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift) && !aiController.isControlling)
                    {
                        MicroTurnRight();
                    }

                    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && !aiController.isControlling)
                    {
                        SuperMicroTurnLeft();
                    }

                    if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && !aiController.isControlling)
                    {
                        SuperMicroTurnRight();
                    }
                }
            }

        }

        if (!isFollowing)
        {
                powerSlider.interactable = false;
        }

    }

    public void MicroTurnLeft()
    {
        this.transform.Translate(new Vector3(adjustSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }

    public void MicroTurnRight()
    {
        this.transform.Translate(new Vector3(-adjustSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }

    public void Reset()
    {
        isFollowing = false;
        isJumpBallMode = false;
    }

    public void SwitchFollowing()
    {
        GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

        if (!isFollowing)
        {
            isFollowing = true;
            if (!gameFlow.isSiegeMode)
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
        }
        else
        {
            isFollowing = false;
            isJumpBallMode = false;
            Cue.GetComponent<CueBehaviour>().PutCueDown();
        }
    }

    private void SwitchToJumpBallMode()
    {
        if (!isJumpBallMode)
        {
            isJumpBallMode = true;
        }
        else
        {
            isJumpBallMode = false;
        }
    }

    private void MaintainJumpBallAngle()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        if (this.transform.position.y - CueBall.transform.position.y < maxCamHeight) //camera height control.
        {
            this.transform.Translate(Vector3.up * Time.deltaTime);
        }
        transform.position = (transform.position - CueBall.transform.position).normalized * minCamDist + CueBall.transform.position;
    }

    public void SuperMicroTurnLeft()
    {
        this.transform.Translate(new Vector3(superMicroMoveSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }

    public void SuperMicroTurnRight()
    {
        this.transform.Translate(new Vector3(-superMicroMoveSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }

}
