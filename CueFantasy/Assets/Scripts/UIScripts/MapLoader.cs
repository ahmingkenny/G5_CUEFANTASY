using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene("Scene3");
    }
    public void LoadScene1AI()
    {
        SceneManager.LoadScene("Scene1AI");
    }

    public void LoadScene2AI()
    {
        SceneManager.LoadScene("Scene2AI");
    }

    public void LoadScene3AI()
    {
        SceneManager.LoadScene("Scene3AI");
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadTutorialScene1()
    {
        SceneManager.LoadScene("TutorialScene1");
    }

    public void LoadTutorialScene2()
    {
        SceneManager.LoadScene("TutorialScene2");
    }

    public void LoadTutorialScene3()
    {
        SceneManager.LoadScene("TutorialScene3");
    }

}
