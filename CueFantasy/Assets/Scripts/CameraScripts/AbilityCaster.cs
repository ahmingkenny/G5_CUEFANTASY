using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityCaster : MonoBehaviour
{
    public bool isCasting = false;
    private GameObject NoticeBoard;
    private NoticeBoard noticeBoard;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private Attacker attacker;
    private Defender defender;
    private int layer_mask;

    [SerializeField] private AudioClip CastSound;

    void Start()
    {
        NoticeBoard = GameObject.Find("NoticeBoard");
        noticeBoard = NoticeBoard.GetComponent<NoticeBoard>();
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
        layer_mask = LayerMask.GetMask("InvisibleWall", "Ability", "Territory");
    }

    void Update()
    {
        if (isCasting)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetButtonUp("Fire1") && ! EventSystem.current.IsPointerOverGameObject())
            {

                if (Physics.Raycast(ray, out hit, 10f, ~layer_mask))
                {

                    if ((hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("TutorialStage")) && hit.collider.gameObject.name != "AbilityButton")
                    {
                        GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                        Cue.GetComponent<IAbility>().CastAbility(hit.point);
                        AudioSource.PlayClipAtPoint(CastSound, this.transform.position);
                    }

                }

            }

            if (Input.GetButtonUp("Fire2"))
            {
                isCasting = false;
                noticeBoard.ShowCancelCasting();
            }

        }

    }

    public void SwitchIsCasting()
    {
        GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

        if (!TopView.isSelecting && !BallShooter.isShoot)
        {
            if ((GameFlow.turn == GameFlow.Turn.Attacker && attacker.mana >= Cue.GetComponent<IAbility>().GetManaCost()) || (GameFlow.turn == GameFlow.Turn.Defender && defender.mana >= Cue.GetComponent<IAbility>().GetManaCost()))
            {
                isCasting = isCasting == false ? true : false;

                if (isCasting)
                {
                    noticeBoard.ShowCastingAbility();
                }

            }

            else if ((GameFlow.turn == GameFlow.Turn.Attacker && attacker.mana < Cue.GetComponent<IAbility>().GetManaCost()) || (GameFlow.turn == GameFlow.Turn.Defender && defender.mana < Cue.GetComponent<IAbility>().GetManaCost()))
            {
                noticeBoard.ShowInsufficientMana();
            }

        }
    }
}
