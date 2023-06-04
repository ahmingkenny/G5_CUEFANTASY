using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryBehaviour : MonoBehaviour
{
    private enum Faction { Attacker, Defender, Neutral};
    private enum BaseType { Main, Sub};

    Color red = new Color(1f, 0.15f, 0f, 0.1f);
    Color blue = new Color(0f, 0.5f, 1f, 0.1f);
    Color purple = new Color(1f, 0f, 0.7f, 0.1f);

    [Header("Info")]
    [SerializeField] private Faction faction;
    [SerializeField] private BaseType baseType;

    [Header("FX")]
    [SerializeField] private GameObject explosionFX;

    [Header("Audio")]
    [SerializeField] private AudioClip explosionSound;

    [Header("GameReference")]
    private GameObject GameManager;
    private Attacker attacker;
    private Defender defender;

    [Header("UIReference")]
    private GameObject EndGameMenu;
    private EndGameMenu endGameMenu;
    private GameObject BattleReporter;
    private BattleReporter battleReporter;
    private GameObject Image;

    void Start()
    {
        Material Aura = GetComponent<Renderer>().material;
        Aura.EnableKeyword("_EMISSION");
        EndGameMenu = GameObject.Find("Canvas").transform.Find("EndGameMenu").gameObject;
        endGameMenu = EndGameMenu.GetComponent<EndGameMenu>();
        GameManager = GameObject.Find("GameManager");
        attacker = GameManager.GetComponent<Attacker>();
        defender = GameManager.GetComponent<Defender>();
        BattleReporter = GameObject.Find("BattleReporter");
        battleReporter = BattleReporter.GetComponent<BattleReporter>();
    }

    void Update()
    {
        if(faction == Faction.Attacker)
        {
            Material Aura = GetComponent<Renderer>().material;
            Aura.SetColor("_EmissionColor", red);

            if (baseType == BaseType.Main && GameObject.FindGameObjectsWithTag("DefenderBall").Length == 0)
            {
                Image = transform.Find("WSCanvas").transform.Find("Image").gameObject;
                Image.SetActive(true);
            }

        }
        else if (faction == Faction.Defender)
        {
            Material Aura = GetComponent<Renderer>().material;
            Aura.SetColor("_EmissionColor", blue);

            if (baseType == BaseType.Main && GameObject.FindGameObjectsWithTag("AttackerBall").Length == 0)
            {
                Image = transform.Find("WSCanvas").transform.Find("Image").gameObject;
                Image.SetActive(true);
            }

        }

        else if (faction == Faction.Neutral)
        {
            Material Aura = GetComponent<Renderer>().material;
            Aura.SetColor("_EmissionColor", purple);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (faction == Faction.Attacker)
        {
            if (collision.collider.tag == "DefenderBall")
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                defender.ballIn++;
                battleReporter.ShowDefenderIn();

                if (baseType != BaseType.Main)
                {
                    faction = Faction.Defender;
                }

            }

            if (collision.collider.tag == "JadeBall" && GameObject.FindGameObjectsWithTag("DefenderBall").Length == 0 && baseType == BaseType.Main)
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                defender.ballIn++;
                battleReporter.ShowDefenderIn();
                EndGameMenu.gameObject.SetActive(true);
                endGameMenu.ShowDefenderWin();
            }

        }

        if (faction == Faction.Defender)
        {
            if (collision.collider.tag == "AttackerBall")
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                attacker.ballIn++;
                battleReporter.ShowAttackerIn();

                if (baseType != BaseType.Main)
                {
                    faction = Faction.Attacker;
                }

            }

            if (collision.collider.tag == "JadeBall" && GameObject.FindGameObjectsWithTag("AttackerBall").Length == 0 && baseType == BaseType.Main)
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                attacker.ballIn++;
                battleReporter.ShowAttackerIn();
                EndGameMenu.gameObject.SetActive(true);
                endGameMenu.ShowAttackerWin();
            }

        }

        if (faction == Faction.Neutral)
        {
            if (collision.collider.tag == "AttackerBall")
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                attacker.ballIn++;
                battleReporter.ShowAttackerIn();

                if (baseType != BaseType.Main)
                {
                    faction = Faction.Attacker;
                }

            }

            if (collision.collider.tag == "DefenderBall")
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                defender.ballIn++;
                battleReporter.ShowDefenderIn();

                if (baseType != BaseType.Main)
                {
                    faction = Faction.Defender;
                }

            }

        }

    }

    public void Neutralize()
    {
        if (baseType != BaseType.Main)
        {
            faction = Faction.Neutral;
        }
    }
}
