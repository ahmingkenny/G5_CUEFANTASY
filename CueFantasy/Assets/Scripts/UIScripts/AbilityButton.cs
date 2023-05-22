using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    private GameObject Text;
    private Text text;

    void Start()
    {
        Text = GameObject.Find("AbilityName");
        text = Text.GetComponent<Text>();
    }
    public void UpdateAbilityName(string name)
    {
        text.text = name;
    }
}
