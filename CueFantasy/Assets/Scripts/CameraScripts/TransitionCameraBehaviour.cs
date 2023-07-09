using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCameraBehaviour : MonoBehaviour
{
    private Vector3 MainCameraPosition;
    private Vector3 OriginalPosition;
    private float lerpSpeed = 0.25f;
    public bool isTransited = false;
    // Start is called before the first frame update
    void Awake()
    {
        OriginalPosition = this.transform.position;
        MainCameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, MainCameraPosition, lerpSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, MainCameraPosition) < 0.5f)
        {
            isTransited = true;
            GetComponent<Camera>().enabled = false;
        }
    }

}
