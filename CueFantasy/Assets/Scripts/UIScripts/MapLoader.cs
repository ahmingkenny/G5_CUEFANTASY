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
}
