using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxValue(int maxValue)
    {
        slider = GetComponent<Slider>();
        slider.maxValue = maxValue;
    }

    public void SetValue(int value)
    {
        slider = GetComponent<Slider>();
        slider.value = value;
    }

}
