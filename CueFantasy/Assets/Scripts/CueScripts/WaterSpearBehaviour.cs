using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpearBehaviour : CueBehaviour, IAbility
{
    //[SerializeField] private int cueNum = 2;
    [SerializeField] private string abilityName = "水牢之獄 (7)";
    [SerializeField] private int manaCost = 7;
    [SerializeField] private GameObject WaterJail;

    [Header("Audio")]
    [SerializeField] private AudioClip WaterSound;

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

        AudioSource.PlayClipAtPoint(WaterSound, this.transform.position);

        if (GameFlow.turn == GameFlow.Turn.Attacker)
        {
            attacker.mana -= manaCost;
            attacker.UpdateManaSlider();
            ShowAbilityIcon();
            Instantiate(WaterJail, targetPosition, Quaternion.identity);
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender)
        {
            defender.mana -= manaCost;
            defender.UpdateManaSlider();
            ShowAbilityIcon();
            Instantiate(WaterJail, targetPosition, Quaternion.identity);
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
    }

    public void ShowAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("WaterSpearIcon").gameObject;
        abilityIcon.SetActive(true);
        Invoke("DisableAbilityIcon", 2.1f);
    }

    public void DisableAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("WaterSpearIcon").gameObject;
        abilityIcon.SetActive(false);
    }

    public int GetManaCost()
    {
        return manaCost;
    }

}
