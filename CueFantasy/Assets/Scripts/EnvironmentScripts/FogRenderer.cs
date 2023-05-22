using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogRenderer : MonoBehaviour
{
    private bool haveFog;
    // Start is called before the first frame update
    void Start()
    {
        haveFog = RenderSettings.fog;
    }

    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = haveFog;
    }
}
