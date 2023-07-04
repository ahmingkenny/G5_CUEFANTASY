using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlow2 : MonoBehaviour
{
    private Vector3 originalPosition;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private GameObject canvas;
    private Attacker attacker;
    private bool isCalled = false;
    private bool isCompleted = false;

    void Start()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        originalPosition = CueBall.transform.position;
        canvas = GameObject.Find("Canvas");
        attacker = GameManager.GetComponent<Attacker>();
    }

    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("AttackerBall").Length == 0)
        {
            isCalled = true;
            isCompleted = true;
            GameObject endTutorialMenu = canvas.transform.Find("EndTutorialMenu").gameObject;
            endTutorialMenu.SetActive(true);
        }

        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");
        if (attacker.mana == 0 && isCalled == false)
        {
            Invoke("CallRestartMenu", 5f);
            isCalled = true;
        }

    }

    void CallRestartMenu()
    {
        if (!isCompleted)
        {
            GameObject restartTutorialMenu = canvas.transform.Find("RestartTutorialMenu").gameObject;
            restartTutorialMenu.SetActive(true);
        }
    }

}
