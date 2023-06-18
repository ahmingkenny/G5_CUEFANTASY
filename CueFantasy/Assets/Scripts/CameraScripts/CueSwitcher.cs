using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueSwitcher : MonoBehaviour
{
    [Header("Game Reference")]
    private GameObject GameManager;
    private GameFlow gameFlow;
    private Attacker attacker;
    private Defender defender;
    private AIController aiController;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
        aiController = GameObject.Find("GameManager").GetComponent<AIController>();
    }

    void Update()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");

        if (Input.GetKeyDown(KeyCode.Alpha1) && !TopView.isViewing && !BallShooter.isShoot && CueBall.GetComponent<Rigidbody>().velocity.z == 0 && !aiController.isControlling)
        {
            if (GameFlow.turn == GameFlow.Turn.Attacker)
            {
                attacker.RespawnCue();
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
            }
            else if (GameFlow.turn == GameFlow.Turn.Defender)
            {
                defender.RespawnCue();
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
            }

            if (CueBallFollower.isFollowing)
            {
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !TopView.isViewing && !BallShooter.isShoot && CueBall.GetComponent<Rigidbody>().velocity.z == 0 && !aiController.isControlling)
        {
            if (GameFlow.turn == GameFlow.Turn.Attacker)
            {
                attacker.RespawnSecondCue();
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
            }
            else if (GameFlow.turn == GameFlow.Turn.Defender)
            {
                defender.RespawnSecondCue();
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
            }

            if (CueBallFollower.isFollowing)
            {
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().TakeCueOut();
            }

        }

    }
}
