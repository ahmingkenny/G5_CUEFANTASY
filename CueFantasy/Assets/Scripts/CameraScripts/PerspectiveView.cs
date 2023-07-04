using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveView : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 4f;
    [SerializeField] private float zoomSpeed = 50f;
    [SerializeField] private float minZoomPos = 0.8f;
    [SerializeField] private float maxZoomPos = 2f;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float microMoveSpeed = 0.5f;

    private Vector3 standardCamDis = new Vector3(0, 1.6f, -1.6f);

    public static Vector3 originalPosition;
    private Vector3 targetPosition;
    private float originalDistance;

    public static bool isLerping = false; //bool variables for controlling steps and sequences of camera movement.
    public static bool isAiming = false;
    public static bool isRecording = true;
    public static bool isSelectLerp = false;

    private AIController aiController;
    private GameObject GameManager;
    private GameFlow gameFlow;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        ResetCameraPosition();
        targetPosition = this.transform.position;
        originalDistance = Vector3.Distance(CueBall.transform.position, this.transform.position);
        aiController = GameObject.Find("GameManager").GetComponent<AIController>();
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
    }

    void Update()
    {

        if (TopView.isViewing || CueBallFollower.isFollowing || isLerping == true || isSelectLerp == true)
        {
            isRecording = false;
        }

        if (!CueBallFollower.isFollowing)
        {

            CueBallFollower.isJumpBallMode = false;

            if (!TopView.isViewing)
            {
                GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
                this.transform.LookAt(CueBall.transform.position);
            }


            if (isRecording) //record the position before switching camera mode.
            {
                originalPosition = this.transform.position;
            }

            if (Input.GetKeyUp(KeyCode.Tab) && !TopView.isSelecting && !TopView.isViewing && !isSelectLerp && !isLerping && !BallShooter.isShoot && !aiController.isControlling)
            {
                isLerping = true;
            }

            if (isLerping)
            {
                Lerping();
            }

            if (isSelectLerp)
            {
                SelectLerp();
            }


            if (Vector3.Distance(targetPosition, this.transform.position) < 0.1)
            {
                isLerping = false;
                isSelectLerp = false;
                isRecording = true; //start record position when camera arrives to the expected position.
            }

            if (!Input.GetKey(KeyCode.Tab) && !isLerping && !isSelectLerp && !TopView.isViewing && !aiController.isControlling)
            {
                GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");

                if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift) && !TopView.isViewing && BallShooter.isShoot == false && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    TurnLeft();
                }

                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift) && !TopView.isViewing && BallShooter.isShoot == false && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    TurnRight();
                }

                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && !TopView.isViewing && BallShooter.isShoot == false && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    MicroTurnLeft();
                }

                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && !TopView.isViewing && BallShooter.isShoot == false && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    MicroTurnRight();
                }

                if (Input.GetAxis("Mouse ScrollWheel") > 0 && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    ZoomIn();
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0 && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    ZoomOut();
                }

                if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    isAiming = false;
                }

                if (Input.GetKey(KeyCode.R) && !TopView.isViewing && BallShooter.isShoot == false && !aiController.isControlling && gameFlow.weaponIsSelected)
                {
                    ResetCameraPosition();
                }

            }

        }

    }

    private void Lerping()
    {
        isSelectLerp = false;
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");

        if (BallShooter.isShoot == false)
        {
            targetPosition = originalPosition;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }

        if (BallShooter.isShoot == true)
        {
            targetPosition = new Vector3(CueBall.transform.position.x + originalDistance, originalPosition.y, CueBall.transform.position.z + originalDistance);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }

    }

    private void SelectLerp()
    {
        isLerping = false;
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        targetPosition = new Vector3(CueBall.transform.position.x, CueBall.transform.position.y + standardCamDis.y, CueBall.transform.position.z + standardCamDis.z);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }

    public void ResetCameraPosition()
    {
        isSelectLerp = true;
    }

    private void ZoomIn()
    {
        isAiming = true;

        if (this.transform.position.y > minZoomPos)
        {
            this.transform.Translate(new Vector3(0, 0, zoomSpeed) * Time.deltaTime, Space.Self);
        }

        isAiming = false;
    }

    private void ZoomOut()
    {
        isAiming = true;

        if (this.transform.position.y < maxZoomPos)
        {
            this.transform.Translate(new Vector3(0, 0, -zoomSpeed) * Time.deltaTime, Space.Self);
        }

        isAiming = false;
    }

    public void TurnLeft()
    {
        isAiming = true;
        this.transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }

    public void MicroTurnLeft()
    {
        isAiming = true;
        this.transform.Translate(new Vector3(microMoveSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }

    public void TurnRight()
    {
        isAiming = true;
        this.transform.Translate(new Vector3(-moveSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }
    public void MicroTurnRight()
    {
        isAiming = true;
        this.transform.Translate(new Vector3(-microMoveSpeed, 0, 0) * Time.deltaTime, Space.Self);
    }
    public void Reset()
    {
        isLerping = false;
        isAiming = false;
        isRecording = true;
        isSelectLerp = false;
    }

}
