using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private float time = 10;
    Text text;
    [SerializeField] GameObject GameoverMenu;
    private GameObject GameManager;
    private GameFlow gameFlow;
    private SiegeEndGameMenu siegeEndGameMenu;
    private bool isCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        siegeEndGameMenu = GameoverMenu.GetComponent<SiegeEndGameMenu>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            if (gameFlow.weaponIsSelected)
            {
                time -= Time.deltaTime;
                text.text = "任務時間剩餘 " + time.ToString("00.00'秒'");
            }
            text.text = "任務時間剩餘 " + time.ToString("00.00'秒'");
        }
        else
        {
            if (!isCalled)
            {
                text.text = "氣數已盡";
                GameoverMenu.SetActive(true);
                Time.timeScale = 0;
                siegeEndGameMenu.ShowLose();
                isCalled = true;
            }
        }

    }

    public void AddTime(float value)
    {
        time += value;
    }

}
