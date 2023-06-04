using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralizerBehaviour : CueBehaviour, IAbility
{
    //[SerializeField] private int cueNum = 5;
    [SerializeField] private string abilityName = "弄喧搗鬼 (8)";
    [SerializeField] private int manaCost = 8;
    [SerializeField] private GameObject Fog;

    [Header("Audio")]
    [SerializeField] private AudioClip LaughSound;

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

        GameObject[] territories;
        territories = GameObject.FindGameObjectsWithTag("SubTerritory");

        float closestDist = 100f;
        GameObject closestTerritory = GameObject.FindGameObjectWithTag("SubTerritory");


        if (territories != null)
        {
            foreach (GameObject territory in territories)
            {
                if (Vector3.Distance(territory.transform.position, targetPosition) < closestDist)
                {
                    closestTerritory = territory;
                    closestDist = Vector3.Distance(territory.transform.position, targetPosition);
                }
            }
        }

        AudioSource.PlayClipAtPoint(LaughSound, this.transform.position);

        if (GameFlow.turn == GameFlow.Turn.Attacker)
        {
            attacker.mana -= manaCost;
            attacker.UpdateManaSlider();
            ShowAbilityIcon();
            closestTerritory.GetComponent<TerritoryBehaviour>().Neutralize();
            Instantiate(Fog, targetPosition, Quaternion.identity);
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender)
        {
            defender.mana -= manaCost;
            defender.UpdateManaSlider();
            ShowAbilityIcon();
            closestTerritory.GetComponent<TerritoryBehaviour>().Neutralize();
            Instantiate(Fog, targetPosition, Quaternion.identity);
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
    }

    public void ShowAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("NeutralizerIcon").gameObject;
        abilityIcon.SetActive(true);
        Invoke("DisableAbilityIcon", 2.1f);
    }

    public void DisableAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("NeutralizerIcon").gameObject;
        abilityIcon.SetActive(false);
    }

    public int GetManaCost()
    {
        return manaCost;
    }
}
