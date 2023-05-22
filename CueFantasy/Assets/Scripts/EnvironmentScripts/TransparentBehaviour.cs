using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentBehaviour : MonoBehaviour
{
    private GameObject MainCamera;
    private Vector3 camPos;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material transparentMaterial;

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        camPos = MainCamera.transform.position;
        if (Vector3.Distance(this.transform.position, camPos) < 3f)
        {
            this.GetComponent<MeshRenderer>().material = transparentMaterial;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = normalMaterial;
        }
    }
}
