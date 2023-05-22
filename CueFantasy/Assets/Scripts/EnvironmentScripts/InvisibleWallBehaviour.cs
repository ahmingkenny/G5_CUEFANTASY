using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallBehaviour : MonoBehaviour
{
    [SerializeField] private float reboundRatio = 0.2f;
    [SerializeField] private float directionalRatio = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "DefenderBall" || collision.collider.tag == "AttackerBall" || collision.collider.tag == "CueBall" || collision.collider.tag == "JadeBall")
        {
            Vector3 direction = collision.gameObject.transform.position - this.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * directionalRatio + collision.gameObject.GetComponent<Rigidbody>().velocity.normalized * reboundRatio, collision.contacts[0].point, ForceMode.Impulse);
        }
    }

}
