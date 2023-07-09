using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FenceBuilding : MonoBehaviour
{
    public bool isBuilding = false;
    private GameObject NoticeBoard;
    private NoticeBoard noticeBoard;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private Attacker attacker;
    private Defender defender;
    private int layer_mask;
    [SerializeField] private GameObject Fence;

    [SerializeField] private AudioClip BuildSound;

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
        if (isBuilding)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetButtonUp("Fire1") && !EventSystem.current.IsPointerOverGameObject())
            {

                if (Physics.Raycast(ray, out hit, 10f, ~layer_mask))
                {

                    if ((hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("TutorialStage")) && hit.collider.gameObject.name != "AbilityButton")
                    {
                        BuildFence(hit.point);
                    }

                }

            }

            if (Input.GetButtonUp("Fire2"))
            {
                isBuilding = false;
                noticeBoard.ShowCancelBuilding();
            }

        }

    }

    public void SwitchIsBuilding()
    {
        GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

        if (!TopView.isSelecting && !BallShooter.isShoot)
        {
                isBuilding = isBuilding == false ? true : false;
        }
    }

    public void BuildFence(Vector3 targetPosition)
    {
        Vector3 finalPosition = targetPosition;
        finalPosition.y += 0.1f;
        Instantiate(Fence, finalPosition, Quaternion.identity);
        InternalAffairs internalAffairs = GameObject.Find("Canvas").transform.Find("InternalAffairsPanel").gameObject.GetComponent<InternalAffairs>();
        isBuilding = false;

        if (GameFlow.turn == GameFlow.Turn.Attacker && attacker.resource >= internalAffairs.BuildCost)
        {
            attacker.ReduceResource(internalAffairs.BuildCost);
            noticeBoard.ShowSuccessBuild();
            GameManager.GetComponent<SoundPlayer>().PlaySuccessSound();
            AudioSource.PlayClipAtPoint(BuildSound, this.transform.position);
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender && defender.resource >= internalAffairs.BuildCost)
        {
            defender.ReduceResource(internalAffairs.BuildCost);
            noticeBoard.ShowSuccessBuild();
            GameManager.GetComponent<SoundPlayer>().PlaySuccessSound();
            AudioSource.PlayClipAtPoint(BuildSound, this.transform.position);
        }

    }

}
