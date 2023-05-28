using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySwordBehaviour : CueBehaviour, IAbility
{
    //[SerializeField] private int cueNum = 5;
    [SerializeField] private string abilityName = "斗轉星移 (5)";
    [SerializeField] private int manaCost = 5;
    [SerializeField] private GameObject GroundFog;

    [Header("Audio")]
    [SerializeField] private AudioClip TeleportSound;

    void Start()
    {
        AbilityButton = GameObject.Find("AbilityButton");
        abilityButton = AbilityButton.GetComponent<AbilityButton>();
        abilityButton.UpdateAbilityName(abilityName);
    }

    void Update()
    {

    }

    public void CastAbility(Vector3 targetPosition)
    {
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
        MainCamera = GameObject.Find("Main Camera");
        abilityCaster = MainCamera.GetComponent<AbilityCaster>();
        NoticeBoard = GameObject.Find("NoticeBoard");
        noticeBoard = NoticeBoard.GetComponent<NoticeBoard>();

        GameObject jadeBall = GameObject.FindGameObjectWithTag("JadeBall");
        GameObject closestBall = jadeBall;
        float closestDist = Vector3.Distance(jadeBall.transform.position, targetPosition);

        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("DefenderBall");

        if (balls != null)
        {
            foreach (GameObject ball in balls)
            {
                if (Vector3.Distance(ball.transform.position, targetPosition) < closestDist)
                {
                    closestBall = ball;
                    closestDist = Vector3.Distance(ball.transform.position, targetPosition);
                }
            }
        }
        

        balls = GameObject.FindGameObjectsWithTag("AttackerBall");
        if (balls != null)
        {
            foreach (GameObject ball in balls)
            {
                if (Vector3.Distance(ball.transform.position, targetPosition) < closestDist)
                {
                    closestBall = ball;
                    closestDist = Vector3.Distance(ball.transform.position, targetPosition);
                }
            }
        }

        AudioSource.PlayClipAtPoint(TeleportSound, this.transform.position);

        if (GameFlow.turn == GameFlow.Turn.Attacker)
        {
            attacker.mana -= manaCost;
            attacker.UpdateManaSlider();
            ShowAbilityIcon();
            GameObject cueBall = GameObject.FindGameObjectWithTag("CueBall");
            Instantiate(GroundFog, cueBall.transform.position, Quaternion.identity);
            closestBall.transform.position = cueBall.transform.position;
            Instantiate(GroundFog, targetPosition, Quaternion.identity);
            cueBall.transform.position = targetPosition;
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender)
        {
            defender.mana -= manaCost;
            defender.UpdateManaSlider();
            ShowAbilityIcon();
            GameObject cueBall = GameObject.FindGameObjectWithTag("CueBall");
            Instantiate(GroundFog, cueBall.transform.position, Quaternion.identity);
            closestBall.transform.position = cueBall.transform.position;
            Instantiate(GroundFog, targetPosition, Quaternion.identity);
            cueBall.transform.position = targetPosition;
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
    }

    public void ShowAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("SkySwordIcon").gameObject;
        abilityIcon.SetActive(true);
        Invoke("DisableAbilityIcon", 2.1f);
    }

    public void DisableAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("SkySwordIcon").gameObject;
        abilityIcon.SetActive(false);
    }

    public int GetManaCost()
    {
        return manaCost;
    }
}
