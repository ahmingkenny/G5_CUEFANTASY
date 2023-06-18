using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CueBallFollower : MonoBehaviour
{
    [SerializeField] private float camSpeed = 4f;
    [SerializeField] private float minCamDist = 0.6f;
    [SerializeField] private float minCamHeight = -0.2f;
    [SerializeField] private float maxCamHeight = 0.4f;
    

    public static bool isFollowing = false;

    private Slider powerSlider;
    private AIController aiController;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<Slider>();
        aiController = GameObject.Find("GameManager").GetComponent<AIController>();
    }

    void Update()
    {
        GameObject CueBall = GameObject.FindGameObjectWithTag("CueBall");

        if (Input.GetKeyDown(KeyCode.Tab) && !TopView.isViewing && !TopView.isSelecting && !PerspectiveView.isSelectLerp && !BallShooter.isShooting && !BallShooter.isShoot && CueBall.GetComponent<Rigidbody>().velocity.z == 0 && !aiController.isControlling)
        {
            GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

            SwitchFollowing();

        }

        if (isFollowing)
        {
            powerSlider.interactable = true;

            this.transform.LookAt(CueBall.transform.position);


            if (Vector3.Distance(this.transform.position, CueBall.transform.position) > minCamDist) //keep the distance between cueball and camera at a range.
            {
                this.transform.position = Vector3.Lerp(this.transform.position, CueBall.transform.position, camSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) && !aiController.isControlling)
            {
                if (this.transform.position.y - CueBall.transform.position.y < maxCamHeight) //camera height control.
                {
                    this.transform.Translate(Vector3.up * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.S) && !aiController.isControlling)
            {
                if (this.transform.position.y - CueBall.transform.position.y > minCamHeight) //camerea height control.
                {
                    this.transform.Translate(Vector3.down * Time.deltaTime);
                }
            }

        }

        if (!isFollowing)
        {
            powerSlider.interactable = false;
        }

    }

    public void Reset()
    {
        isFollowing = false;
    }

    public void SwitchFollowing()
    {
        GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

        if (!isFollowing)
        {
            isFollowing = true;
            Cue.GetComponent<CueBehaviour>().TakeCueOut();
        }
        else
        {
            isFollowing = false;
            Cue.GetComponent<CueBehaviour>().PutCueDown();
        }
    }

}
