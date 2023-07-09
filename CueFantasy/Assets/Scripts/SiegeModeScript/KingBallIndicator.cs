using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBallIndicator : MonoBehaviour
{
    private float maxValue = 1f;
    private float minValue = 0f;
    private const float startValue = 0f;
    private float valueInterval = 0.1f;
    private float timeInterval = 0.05f;
    private float currentValue;
    private bool isTransparent = false;
    private Renderer myRenderer;
    private Color color;

    void Start()
    {
        currentValue = startValue;
        color = new Color(0, 0, 0, startValue);
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.SetColor("_Color", color);
        Recursion();
    }

    void Update()
    {

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
        color = new Color(1, 0, 0, currentValue);
        myRenderer.material.SetColor("_Color", color);
        Recursion();
    }

    private void DecreaseValue()
    {
        currentValue -= valueInterval;
        color = new Color(1, 0, 0, currentValue);
        myRenderer.material.SetColor("_Color", color);
        Recursion();
    }

}