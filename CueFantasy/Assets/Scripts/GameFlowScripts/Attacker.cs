using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacker : MonoBehaviour
{

    public static int CueNum = 1;
    public static int SecondCueNum = 1;
    public int mana = 0;
    public int manaPerTurn = 1;
    public int internalPoint = 0;
    public int resource = 0;
    public int legalHit = 0;
    public int illegalHit = 0;
    public int foul = 0;
    public int abilityUsed = 0;
    public int ballIn = 0;

    private int manaLimit = 10;

    private Slider manaSlider;
    private GameObject MainCamera;
    private CueSpawner cueSpawner;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
    }

    void Update()
    {
        
    }

    public void SelectCue(int num)
    {
        CueNum = num;
    }

    public void SelectSecondCue(int num)
    {
        SecondCueNum = num;
    }
    public void GainResource(int amount)
    {
        resource += amount;
        UpdateResource();
    }

    public void GainMana()
    {
        if (mana < manaLimit)
        {
            mana++;
        }
        UpdateManaSlider();
    }

    public void GainPoint()
    {
        internalPoint++;
        UpdatePoint();
    }

    public void UpdateManaSlider()
    {
        manaSlider.value = mana;
    }

    public void Reset()
    {
        CueNum = 1;
        SecondCueNum = 1;
    }

    public void RespawnCue() 
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cueSpawner = MainCamera.GetComponent<CueSpawner>();
        cueSpawner.RespawnCue(CueNum);
    }

    public void RespawnSecondCue()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cueSpawner = MainCamera.GetComponent<CueSpawner>();
        cueSpawner.RespawnCue(SecondCueNum);
    }

    public void UpdatePoint()
    {
        GameObject.Find("InternalAffairs").GetComponent<Text>().text = "內政點 " + internalPoint;
    }

    public void UpdateResource()
    {
        GameObject.Find("Resources").GetComponent<Text>().text = "資源 " + resource;
    }

    public void ReducePoint(int amount)
    {
        internalPoint -= amount;
        UpdatePoint();
    }

    public void ReduceResource(int amount)
    {
        resource -= amount;
        UpdateResource();
    }

}
