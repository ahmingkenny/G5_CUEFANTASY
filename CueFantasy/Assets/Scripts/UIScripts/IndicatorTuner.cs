using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorTuner : MonoBehaviour
{
    [SerializeField] private float startValue = 0;
    [SerializeField] private float valueInterval = 0.005f;
    private Color color;
    private float currentValue;
    private Renderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(1, 0, 0, startValue);
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.SetColor("_Color", color);
    }

    // Update is called once per frame
    void Update()
    {
        color.a += valueInterval;
        myRenderer.material.SetColor("_Color", color);
    }
}
