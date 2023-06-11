using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTerritoryBehaviour : MonoBehaviour
{
    private enum Faction { Other };

    Color gold = new Color(1f, 1f, 0f, 0.1f);

    [Header("Info")]
    [SerializeField] private Faction faction;

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
        if (faction == Faction.Other)
        {
            Material Aura = GetComponent<Renderer>().material;
            Aura.SetColor("_EmissionColor", gold);

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (faction == Faction.Other)
        {
            if (collision.collider.tag == "DefenderBall")
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                defender.ballIn++;
                battleReporter.ShowDefenderIn();
                defender.GainMana();
                defender.GainMana();
            }

            if (collision.collider.tag == "AttackerBall")
            {

                Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
                collision.gameObject.GetComponent<IDestroyable>().DestroyIt();
                attacker.ballIn++;
                battleReporter.ShowAttackerIn();
                attacker.GainMana();
                attacker.GainMana();
            }
        }

    }
}
