using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingController : MonoBehaviour
{
    private float maxValue = 1f;
    private float minValue = 0f;
    private const float startValue = 0f;
    private float valueInterval = 0.1f;
    private float timeInterval = 0.05f;
    private float currentValue;
    private bool isTransparent = false;
    private bool isCalled = false;
    [SerializeField] private float flashTime = 1.5f;

    void Start()
    {
        currentValue = startValue;
        Recursion();
    }

    void Update()
    {

        if (GetComponent<Image>().enabled == true && !isCalled)
        {
            Invoke("Sleep", flashTime);
            isCalled = true;
        }

        if (currentValue >= maxValue)
        {
            isTransparent = false;
        }

        if (currentValue <= minValue)
        {
            isTransparent = true;
        }

    }

    private void Recursion()
    {

        if (isTransparent)
        {
            Invoke("IncreaseValue", timeInterval);
        }
        else if (!isTransparent)
        {
            Invoke("DecreaseValue", timeInterval);
        }
    }

    private void IncreaseValue()
    {
        currentValue += valueInterval;
        Color color = new Color(1, 0, 0, currentValue);
        GetComponent<Image>().color = color;
        Recursion();
    }

    private void DecreaseValue()
    {
        currentValue -= valueInterval;
        Color color = new Color(1, 0, 0, currentValue);
        GetComponent<Image>().color = color;
        Recursion();
    }

    private void Sleep()
    {
        GetComponent<Image>().enabled = false;
        isCalled = false;
    }
    public void WakeUp()
    {
        GetComponent<Image>().enabled = true;
    }

}