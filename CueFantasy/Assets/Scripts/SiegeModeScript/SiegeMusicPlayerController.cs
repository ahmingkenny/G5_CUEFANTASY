using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeMusicPlayerController : MonoBehaviour
{
    private GameObject GameManager;
    private SiegeModeAIController siegeModeAIController;
    private bool isDisabled = false;
    private bool isCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        siegeModeAIController = GameManager.GetComponent<SiegeModeAIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (siegeModeAIController.isPhase2 == true && !isDisabled)
        {
            GetComponent<AudioSource>().spatialBlend += 0.012f;
            if (!isCalled)
            {
                GameObject.Find("MusicPlayer2").GetComponent<AudioSource>().enabled = true;
                Invoke("DisableBgm", 3f);
                isCalled = true;
            }
        }

    }

    private void DisableBgm()
    {
        GetComponent<AudioSource>().enabled = false;
        isDisabled = true;
    }

}
