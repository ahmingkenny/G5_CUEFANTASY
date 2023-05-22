using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerSlash : MonoBehaviour, IDestroyable
{
    [SerializeField] private float power = 0.3f;
    [SerializeField] private int damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyIt" , 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "CueBall" || other.tag == "AttackerBall" || other.tag == "DefenderBall" || other.tag == "JadeBall")
        {
                Vector3 direction = other.ClosestPointOnBounds(transform.position) - this.transform.position;
                other.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * power, other.ClosestPointOnBounds(transform.position), ForceMode.Impulse);
        }

        if (other.tag == "Destructible")
        {
            other.GetComponent<IDamageable>().Hit(damage);
        }

    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }

}
