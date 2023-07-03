using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    private float maxIntensity = 1.8f;
    private float minIntensity = 0.135f;
    private const float startIntensity = 1;
    private float intensityInterval = 0.0185f;
    private float timeInterval = 1f;
    private float currentIntensity;
    //the value and interval are matching with the directional light animation.
    private bool isNight = false;

    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
        currentIntensity = myLight.intensity;
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
        myLight.intensity = currentIntensity;
        Recursion();
    }

    private void DecreaseIntensity()
    {
        currentIntensity -= intensityInterval;
        myLight.intensity = currentIntensity;
        Recursion();
    }
}
