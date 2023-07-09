using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalAffairs : MonoBehaviour
{
    private GameObject GameManager;
    private GameFlow gameFlow;
    private Attacker attacker;
    private Defender defender;
    private GameObject NoticeBoard;
    private NoticeBoard noticeBoard;
    private SoundPlayer soundPlayer;

    public int ManaPerRoundCost = 7;
    public int GainManaCost = 3;
    public int GainResourceCost = 1;
    public int GainResourceAmount = 400;
    public int BuildCost = 1000;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
        NoticeBoard = GameObject.Find("NoticeBoard");
        noticeBoard = NoticeBoard.GetComponent<NoticeBoard>();
        soundPlayer = GameManager.GetComponent<SoundPlayer>();
    }
    public void IncreaseManaPerRound()
    {
        
        if (GameFlow.turn == GameFlow.Turn.Attacker && attacker.internalPoint >= ManaPerRoundCost)
        {
            attacker.manaPerTurn++;
            attacker.ReducePoint(ManaPerRoundCost);
            noticeBoard.ShowEnhaneManaPerRound();
            soundPlayer.PlaySuccessSound();
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender && defender.internalPoint >= ManaPerRoundCost)
        {
            defender.manaPerTurn++;
            defender.ReducePoint(ManaPerRoundCost);
            noticeBoard.ShowEnhaneManaPerRound();
            soundPlayer.PlaySuccessSound();
        }
        else
        {
            noticeBoard.ShowInsufficientPoint();
        }

    }

    public void GainMana()
    {
        if (GameFlow.turn == GameFlow.Turn.Attacker && attacker.internalPoint >= GainManaCost)
        {
            attacker.GainMana();
            attacker.GainMana();
            attacker.ReducePoint(GainManaCost);
            noticeBoard.ShowGainMana();
            soundPlayer.PlaySuccessSound();
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender && defender.internalPoint >= GainManaCost)
        {
            defender.GainMana();
            defender.GainMana();
            defender.ReducePoint(GainManaCost);
            noticeBoard.ShowGainMana();
            soundPlayer.PlaySuccessSound();
        }
        else
        {
            noticeBoard.ShowInsufficientPoint();
        }

    }

    public void GainResource()
    {
        if (GameFlow.turn == GameFlow.Turn.Attacker && attacker.internalPoint >= GainResourceCost)
        {
            attacker.GainResource(GainResourceAmount);
            attacker.ReducePoint(GainResourceCost);
            noticeBoard.ShowGainResource();
            soundPlayer.PlaySuccessSound();
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender && defender.internalPoint >= GainResourceCost)
        {
            defender.GainResource(GainResourceAmount);
            defender.ReducePoint(GainResourceCost);
            noticeBoard.ShowGainResource();
            soundPlayer.PlaySuccessSound();
        }
        else
        {
            noticeBoard.ShowInsufficientPoint();
        }

    }

    public void BuildFence()
    {
        if (GameFlow.turn == GameFlow.Turn.Attacker && attacker.resource >= BuildCost && !TopView.isSelecting && !BallShooter.isShoot)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FenceBuilding>().SwitchIsBuilding();
            noticeBoard.ShowAskBuildLocation();
            this.gameObject.SetActive(false);
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender && defender.resource >= BuildCost && !TopView.isSelecting && !BallShooter.isShoot)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FenceBuilding>().SwitchIsBuilding();
            noticeBoard.ShowAskBuildLocation();
            this.gameObject.SetActive(false);
        }
        else
        {
            noticeBoard.ShowInsufficientResource();
        }
    }

}
