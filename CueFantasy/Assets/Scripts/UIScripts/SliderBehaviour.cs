using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] float minValue = 0.3f;
    [SerializeField] float maxValue = 2f;

    public void EnhancePower(float multiplier)
    {
        Slider slider = GetComponent<Slider>();
        slider.maxValue = maxValue * multiplier;
    }

    public void ResetValue()
    {
        Slider slider = GetComponent<Slider>();
        slider.maxValue = maxValue;
    }

}
