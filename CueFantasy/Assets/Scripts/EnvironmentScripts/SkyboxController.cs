using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    private float maxIntensity = 1f;
    private float minIntensity = 0.1f;
    private const float startIntensity = 1;
    private float intensityInterval = 0.01f;
    private float timeInterval = 1f;
    private float currentIntensity;
    //the value and interval are matching with the directional light animation.
    private bool isNight = false;

    void Start()
    {
        RenderSettings.skybox.SetFloat("_Exposure", startIntensity);
        currentIntensity = startIntensity;
        Recursion();
    }

    void Update()
    {
        if (currentIntensity >= maxIntensity)
        {
            isNight = false;
        }

        if (currentIntensity <= minIntensity)
        {
            isNight = true;
        }

    }


    private void Recursion()
    {
        if (isNight)
        {
            Invoke("IncreaseIntensity", timeInterval);
        }
        else if (!isNight)
        {
            Invoke("DecreaseIntensity", timeInterval);
        }
    }

    private void IncreaseIntensity()
    {
        currentIntensity += intensityInterval;
        RenderSettings.skybox.SetFloat("_Exposure", currentIntensity);
        Recursion();
    }

    private void DecreaseIntensity()
    {
        currentIntensity -= intensityInterval;
        RenderSettings.skybox.SetFloat("_Exposure", currentIntensity);
        Recursion();
    }
}
