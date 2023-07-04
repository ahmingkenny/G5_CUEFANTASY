using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlow3 : MonoBehaviour
{
    private Vector3 originalPosition;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private GameObject canvas;
    private Attacker attacker;
    private GameObject MainCamera;
    private TopView topView;
    private PerspectiveView perspectiveView;
    private bool isPassed = false;

    void Start()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        originalPosition = CueBall.transform.position;
        canvas = GameObject.Find("Canvas");
        attacker = GameManager.GetComponent<Attacker>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        topView = MainCamera.GetComponent<TopView>();
        perspectiveView = MainCamera.GetComponent<PerspectiveView>();
    }

    void Update()
    {
        if (gameFlow.weaponIsSelected && Input.GetKeyDown(KeyCode.Tab) && isPassed == false && !TopView.isViewing && !PerspectiveView.isLerping)
        {
            GameObject mission1 = canvas.transform.Find("Mission1").gameObject;
            mission1.SetActive(false);
            GameObject mission2 = canvas.transform.Find("Mission2").gameObject;
            mission2.SetActive(true);
            isPassed = true;
        }
        else if (gameFlow.weaponIsSelected && Input.GetKeyDown(KeyCode.Tab) && isPassed == true && !TopView.isViewing && !PerspectiveView.isLerping)
        {
            GameObject mission1 = canvas.transform.Find("Mission1").gameObject;
            mission1.SetActive(true);
            GameObject mission2 = canvas.transform.Find("Mission2").gameObject;
            mission2.SetActive(false);
            isPassed = false;
        }

        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        if (attacker.foul == 1 || (attacker.legalHit > 0 && GameObject.FindGameObjectsWithTag("JadeBall").Length == 1 && CueBall.GetComponent<Rigidbody>().velocity.z == 0))
        {
            GameObject restartTutorialMenu = canvas.transform.Find("RestartTutorialMenu").gameObject;
            restartTutorialMenu.SetActive(true);
        }

    }

}