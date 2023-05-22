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

}
