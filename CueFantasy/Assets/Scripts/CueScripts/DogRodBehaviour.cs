using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogRodBehaviour : CueBehaviour, IAbility
{
    //[SerializeField] private int cueNum = 4;
    [SerializeField] private string abilityName = "天下無狗 (9)";
    [SerializeField] private int manaCost = 9;
    [SerializeField] private GameObject BlackHole;

    [Header("Audio")]
    [SerializeField] private AudioClip ThunderSound;

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

        AudioSource.PlayClipAtPoint(ThunderSound, this.transform.position);

        if (GameFlow.turn == GameFlow.Turn.Attacker)
        {
            attacker.mana -= manaCost;
            attacker.UpdateManaSlider();
            ShowAbilityIcon();
            Instantiate(BlackHole, targetPosition, Quaternion.identity);
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
        else if (GameFlow.turn == GameFlow.Turn.Defender)
        {
            defender.mana -= manaCost;
            defender.UpdateManaSlider();
            ShowAbilityIcon();
            Instantiate(BlackHole, targetPosition, Quaternion.identity);
            noticeBoard.ShowSuccessCasting();
            abilityCaster.isCasting = false;
        }
    }

    public void ShowAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("DogRodIcon").gameObject;
        abilityIcon.SetActive(true);
        Invoke("DisableAbilityIcon", 2.1f);
    }

    public void DisableAbilityIcon()
    {
        GameObject AbilityIcon = GameObject.Find("AbilityIcon");
        GameObject abilityIcon = AbilityIcon.transform.Find("DogRodIcon").gameObject;
        abilityIcon.SetActive(false);
    }

    public int GetManaCost()
    {
        return manaCost;
    }

}
