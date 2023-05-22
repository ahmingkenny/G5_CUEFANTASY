using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFXSwitch : MonoBehaviour
{
    public Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<ParticleSystemRenderer>();
    }

    public void RendererEnable()
    {
        renderer.enabled = true;
    }

    public void RendererDisable()
    {
        renderer.enabled = false;
    }

}
