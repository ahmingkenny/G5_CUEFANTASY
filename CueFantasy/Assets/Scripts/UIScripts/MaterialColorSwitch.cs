using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorSwitch : MonoBehaviour
{
    private ParticleSystem particleSystem;
    Color red = new Color(0.75f, 0.3f, 0.03f);
    Color blue = new Color(0f, 0.3f, 0.75f);

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void ChangeToRed()
    {
        Material material = particleSystem.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", red*5f);
    }

    public void ChangeToBlue()
    {
        Material material = particleSystem.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", blue*5f);
    }
}
