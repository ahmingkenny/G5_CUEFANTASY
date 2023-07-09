using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public void LoadScene1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene1");
    }

    public void LoadScene2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene2");
    }

    public void LoadScene3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene3");
    }
    public void LoadScene1AI()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene1AI");
    }

    public void LoadScene2AI()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene2AI");
    }

    public void LoadScene3AI()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene3AI");
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadTutorialScene1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TutorialScene1");
    }

    public void LoadTutorialScene2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TutorialScene2");
    }

    public void LoadTutorialScene3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TutorialScene3");
    }

    public void LoadSiegeScene1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SiegeScene1");
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
