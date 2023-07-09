using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    [SerializeField] public int turnLimit = 60;
    private const int startTurnNum = 1;
    public int turnNum;
    public bool isNextTurn = false;
    public bool isEnd = false;
    [SerializeField] public bool isAIActived;
    [SerializeField] public bool isSiegeMode;
    public bool weaponIsSelected = false;
    private int resourcePerTurn = 100;

    public enum Turn { Attacker, Defender};
    public static Turn turn;
    public enum AISide { Attacker, Defender };
    [SerializeField] public AISide aiSide;

    private Vector3 cueBallPos;

    [Header("Game Reference")]
    private GameObject MainCamera;
    private BallShooter ballShooter;
    private TopView topView;
    private PerspectiveView perspectiveView;
    private CueSpawner cueSpawner;
    private GameObject CueBall;
    private CueBallBehaviour cueBallBehaviour;
    private CueBallFollower cueBallFollower;
    private Attacker attacker;
    private Defender defender;
    private AbilityCaster abiliterCaster;
    private RandomEventSystem randomEventSystem;
    private AIController aIController;

    [Header("UI Reference")]
    private Image DefIcon;
    private Image AtkIcon;
    private Text TurnText;
    private TurnTextSwitch turnTextSwitch;
    private ParticleSystem Fire;
    private ParticleSystem Ember;
    private MaterialColorSwitch fireColorSwitch;
    private MaterialColorSwitch emberColorSwitch;
    private GameObject EndGameMenu;
    private EndGameMenu endGameMenu;
    private GameObject NoticeBoard;
    private NoticeBoard noticeBoard;
    private GameObject powerSlider;
    private SliderBehaviour sliderBehaviour;
    private GameObject BattleReporter;
    private BattleReporter battleReporter;
    private FenceBuilding fenceBuilding;
    private AbilityCaster abilityCaster;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        turnNum = startTurnNum;

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ballShooter = MainCamera.GetComponent<BallShooter>();
        perspectiveView = MainCamera.GetComponent<PerspectiveView>();
        topView = MainCamera.GetComponent<TopView>();
        cueSpawner = MainCamera.GetComponent<CueSpawner>();
        CueBall = GameObject.FindGameObjectWithTag("CueBall");
        cueBallBehaviour = CueBall.GetComponent<CueBallBehaviour>();
        attacker = GetComponent<Attacker>();
        defender = GetComponent<Defender>();
        randomEventSystem = GetComponent<RandomEventSystem>();
        aIController = GetComponent<AIController>();
        cueBallFollower = MainCamera.GetComponent<CueBallFollower>();
        fenceBuilding = MainCamera.GetComponent<FenceBuilding>();
        abilityCaster = MainCamera.GetComponent<AbilityCaster>();

        DefIcon = GameObject.Find("DefIcon").GetComponent<Image>();
        AtkIcon = GameObject.Find("AtkIcon").GetComponent<Image>();
        TurnText = GameObject.Find("TurnText").GetComponent<Text>();
        turnTextSwitch = TurnText.GetComponent<TurnTextSwitch>();
        Fire = GameObject.Find("Fire").GetComponent<ParticleSystem>();
        fireColorSwitch = Fire.GetComponent<MaterialColorSwitch>();
        Ember = GameObject.Find("EmbersColor").GetComponent<ParticleSystem>();
        emberColorSwitch = Ember.GetComponent<MaterialColorSwitch>();
        EndGameMenu = GameObject.Find("Canvas").transform.Find("EndGameMenu").gameObject;
        endGameMenu = EndGameMenu.GetComponent<EndGameMenu>();
        NoticeBoard = GameObject.Find("NoticeBoard");
        noticeBoard = NoticeBoard.GetComponent<NoticeBoard>();
        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider");
        sliderBehaviour = powerSlider.GetComponent<SliderBehaviour>();
        BattleReporter = GameObject.Find("BattleReporter");
        battleReporter = BattleReporter.GetComponent<BattleReporter>();

        isNextTurn = true;
        turn = Turn.Attacker;

        if (isSiegeMode)
        {
            CueBallFollower.isFollowing = true;
        }
    }

    void Update()
    {
        if (CueBall.GetComponent<Rigidbody>().velocity.z == 0 && CueBall.GetComponent<Rigidbody>().velocity.y == 0 && !BallShooter.isShoot) //velocity.y checking is mostly for checking whether the ball has become static after reposition of cue ball when player has been foul.
        {
            cueBallPos = CueBall.transform.position;
        }

        if (turnNum != 0 && turnNum % 2 != 0 && turnNum <= turnLimit && isNextTurn && !BallShooter.isShoot && weaponIsSelected)
        {

            if (isAIActived && aiSide == AISide.Attacker)
            {
                aIController.ActiveAIController();
            }
            else
            {
                GameObject Text = GameObject.Find("AITurnCover").transform.Find("Text").gameObject;
                Text.GetComponent<Text>().enabled = false;
                GameObject aiTurnCover = GameObject.Find("AITurnCover");
                aiTurnCover.GetComponent<Image>().enabled = false;
                
            }

            if (!TopView.isSelecting)
            {
                noticeBoard.ShowAttackerTurn();
            }
            turnTextSwitch.UpdateTurnText();
            sliderBehaviour.ResetValue();
            GameObject.Find("Canvas").transform.Find("InternalAffairsPanel").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("GameBook").gameObject.SetActive(false);
            for (int i = 0; i < attacker.manaPerTurn; i++)
            {
                attacker.GainMana();
            }
            attacker.GainPoint();
            attacker.GainResource(resourcePerTurn);
            abilityCaster.isCasting = false;
            fenceBuilding.isBuilding = false;
            DefIcon.enabled = false;
            AtkIcon.enabled = true;
            ShowEvent();
            attacker.RespawnCue();
            if (!isSiegeMode)
                randomEventSystem.CreateEvent();
            emberColorSwitch.ChangeToRed();
            fireColorSwitch.ChangeToRed();
            cueBallBehaviour.InitializeData();
            isNextTurn = false;
            perspectiveView.ResetCameraPosition();
        }
        else if (turnNum != 0 && turnNum % 2 == 0 && turnNum <= turnLimit && isNextTurn && !BallShooter.isShoot && weaponIsSelected)
        {
            if (isAIActived && aiSide == AISide.Defender)
            {
                aIController.ActiveAIController();
            }
            else
            {
                GameObject Text = GameObject.Find("AITurnCover").transform.Find("Text").gameObject;
                Text.GetComponent<Text>().enabled = false;
                GameObject aiTurnCover = GameObject.Find("AITurnCover");
                aiTurnCover.GetComponent<Image>().enabled = false;
            }

            if (!TopView.isSelecting)
            {
                noticeBoard.ShowDefenderTurn();
            }
            turnTextSwitch.UpdateTurnText();
            sliderBehaviour.ResetValue();
            GameObject.Find("Canvas").transform.Find("InternalAffairsPanel").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("GameBook").gameObject.SetActive(false);
            for (int i = 0; i < defender.manaPerTurn; i++)
            {
                defender.GainMana();
            }
            defender.GainPoint();
            defender.GainResource(resourcePerTurn);
            abilityCaster.isCasting = false;
            fenceBuilding.isBuilding = false;
            AtkIcon.enabled = false;
            DefIcon.enabled = true;
            ShowEvent();
            defender.RespawnCue();
            if (!isSiegeMode)
                randomEventSystem.CreateEvent();
            emberColorSwitch.ChangeToBlue();
            fireColorSwitch.ChangeToBlue();
            cueBallBehaviour.InitializeData();
            isNextTurn = false;
            perspectiveView.ResetCameraPosition();
        }

        if (BallShooter.isShoot == true && CueBall.transform.position != cueBallPos && CueBall.GetComponent<Rigidbody>().velocity.z == 0 && isEnd == false && !isSiegeMode) // isShoot is for checking whether player has shoot the cue ball or not
            //Adding cue ball position checking secure turn change mechansim. Velocity checking alone has deviation as ball can be hit and has velocity as 0 at the same time.
        {
            GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

            if (Cue != null)
            {
                Cue.GetComponent<IDestroyable>().DestroyIt();
            }

            if (CueBallBehaviour.legalHit == 0)
            {
                if (turn == Turn.Attacker)
                {
                    attacker.foul++;
                }
                else if (turn == Turn.Defender)
                {
                    defender.foul++;
                }
            }

            if (turnNum == turnLimit) //Defender win the game when the last turn ended;
            {
                EndGameMenu.gameObject.SetActive(true);
                endGameMenu.ShowDefenderWin();
            }

            turn = turn == Turn.Attacker ? Turn.Defender : Turn.Attacker;

            BallShooter.isShoot = false;
            battleReporter.clearText();
            turnNum++;
            isNextTurn = true;

            if (CueBallBehaviour.legalHit == 0 && CueBall.GetComponent<Rigidbody>().velocity.z == 0)
            {
                if (turn == Turn.Attacker)
                {
                    noticeBoard.ShowDefenderFoul();
                }
                else if (turn == Turn.Defender)
                {
                    noticeBoard.ShowAttackerFoul();
                }

                TopView.isSelecting = true;
            }

        }

    }

    public void WeaponIsSelected()
    {
        weaponIsSelected = true;
    }

    private void ShowEvent()
    {
        GameObject[] Terrain = GameObject.FindGameObjectsWithTag("Terrain");
        for (int i = 0; i < Terrain.Length; i++)
        {
            Terrain[i].GetComponent<TerrainEventBehaviour>().ShowEvent();
        }
    }
    public void Reset()
    {
        turn = Turn.Attacker;
    }

}
