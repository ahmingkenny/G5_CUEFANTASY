using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsProvider : MonoBehaviour
{
    private GameObject NoticeBoard;
    private NoticeBoard noticeBoard;
    private int layer_mask;

    // Start is called before the first frame update
    void Start()
    {
        NoticeBoard = GameObject.Find("NoticeBoard");
        noticeBoard = NoticeBoard.GetComponent<NoticeBoard>();
        layer_mask = LayerMask.GetMask("Ability");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonUp("Fire1"))
        {

            if (Physics.Raycast(ray, out hit, 10f, ~layer_mask))
            {

                if (hit.collider.name == "advisorAtk" || hit.collider.name == "advisorDef")
                {
                    noticeBoard.ShowAdvisorType();
                }
                else if (hit.collider.name == "soldierAtk" || hit.collider.name == "soldierDef")
                {
                    noticeBoard.ShowSoldierType();
                }
                else if (hit.collider.name == "archerAtk" || hit.collider.name == "archerDef")
                {
                    noticeBoard.ShowArcherType();
                }
                else if (hit.collider.name == "horseAtk" || hit.collider.name == "horseDef")
                {
                    noticeBoard.ShowHorseType();
                }
                else if (hit.collider.name == "jade")
                                {
                    noticeBoard.ShowJadeType();
                }
                else if (hit.collider.name == "MongoliaHorse")
                {
                    noticeBoard.ShowMongoliaHorseType();
                }

                else if (hit.collider.name == "King")
                {
                    noticeBoard.ShowKingType();
                }
                else if (hit.collider.name == "Yurt")
                {
                    noticeBoard.ShowYurtType();
                }
                else if (hit.collider.name == "SiegeBoss")
                {
                    noticeBoard.ShowSiegeBossType();
                }
                else if (hit.collider.name == "Fence")
                {
                    noticeBoard.ShowFenceType();
                }
            }
        }
    }
}
