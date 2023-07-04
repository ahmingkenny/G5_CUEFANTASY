using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    private GameObject Text;
    private Text text;

    private GameObject GameManager;
    private GameFlow gameFlow;
    private SoundPlayer soundPlayer;
    [SerializeField] bool isLose;
    [SerializeField] bool isTutorial;

    // Start is called before the first frame update
    void Awake()
    {
        Text = this.transform.Find("Text").gameObject;
        text = Text.GetComponent<Text>();
    }

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        gameFlow = GameManager.GetComponent<GameFlow>();
        gameFlow.isEnd = true;
        soundPlayer = GameManager.GetComponent<SoundPlayer>();
        if (!isLose)
        {
            soundPlayer.PlayEndGameSound();
        }
        else
        {
            soundPlayer.PlayLoseSound();
        }
    }

    public void ShowAttackerWin()
    {
        if (isTutorial)
        {
            text.text = "教學模式完成";
        }
        else
            text.text = "進攻方獲勝";
        Time.timeScale = 0;
    }

    public void ShowDefenderWin()
    {
        text.text = "防守方獲勝";
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
