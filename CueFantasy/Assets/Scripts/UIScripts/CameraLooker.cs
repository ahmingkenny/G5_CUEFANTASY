using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooker : MonoBehaviour
{
    private GameObject MainCamera;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(MainCamera.transform.position);
    }
}
