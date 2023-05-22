using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorBallPassive : MonoBehaviour
{
    [SerializeField] private float massMultiplier = 1.5f;
    [SerializeField] private float dragMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

        if (transform.parent.tag == "AttackerBall")
        {

            if (other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
            {
                other.gameObject.GetComponent<Rigidbody>().mass *= massMultiplier;
                other.gameObject.GetComponent<Rigidbody>().drag *= dragMultiplier;
            }

        }

        if (transform.parent.tag == "DefenderBall")
        {

            if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "JadeBall")
            {
                other.gameObject.GetComponent<Rigidbody>().mass *= massMultiplier;
                other.gameObject.GetComponent<Rigidbody>().drag *= dragMultiplier;
            }

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (transform.parent.tag == "AttackerBall")
        {

            if (other.gameObject.tag == "DefenderBall" || other.gameObject.tag == "JadeBall")
            {
                other.gameObject.GetComponent<Rigidbody>().mass /= massMultiplier;
                other.gameObject.GetComponent<Rigidbody>().drag /= dragMultiplier;
            }

        }

        if (transform.parent.tag == "DefenderBall")
        {

            if (other.gameObject.tag == "AttackerBall" || other.gameObject.tag == "JadeBall")
            {
                other.gameObject.GetComponent<Rigidbody>().mass /= massMultiplier;
                other.gameObject.GetComponent<Rigidbody>().drag /= dragMultiplier;
            }

        }

    }

}
