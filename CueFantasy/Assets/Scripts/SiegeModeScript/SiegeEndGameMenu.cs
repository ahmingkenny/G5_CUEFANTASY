using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiegeEndGameMenu : MonoBehaviour
{
    private GameObject GameManager;
    private SoundPlayer soundPlayer;
    private GameObject Text;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowWin()
    {
        Text = this.transform.Find("Text").gameObject;
        text = Text.GetComponent<Text>();
        GameManager = GameObject.Find("GameManager");
        soundPlayer = GameManager.GetComponent<SoundPlayer>();
        soundPlayer.PlayEndGameSound();
        text.text = "成攻討伐前朝餘孽";
        Time.timeScale = 0;
    }

    public void ShowLose()
    {
        Text = this.transform.Find("Text").gameObject;
        text = Text.GetComponent<Text>();
        GameManager = GameObject.Find("GameManager");
        soundPlayer = GameManager.GetComponent<SoundPlayer>();
        soundPlayer.PlayLoseSound();
        text.text = "餘孽未盡後患無窮";
        Time.timeScale = 0;
    }

}
