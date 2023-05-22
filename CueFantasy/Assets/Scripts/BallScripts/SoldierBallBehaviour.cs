using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBallBehaviour : MonoBehaviour, IDestroyable
{
    [SerializeField] private float reflectRatio = 0.1f;
    [SerializeField] private float directionalRatio = 0.3f;
    [SerializeField] private int damage = 5;

    [Header("Audio")]
    [SerializeField] private AudioClip TerrainImpactSound;
    [SerializeField] private AudioClip TroopsImpactSound;

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

        if (collision.collider.tag == "CueBall" || collision.collider.tag == "AttackerBall" || collision.collider.tag == "DefenderBall" || collision.collider.tag == "JadeBall")
        {
            Vector3 direction = this.transform.position - collision.gameObject.transform.position;
            GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * directionalRatio + collision.gameObject.GetComponent<Rigidbody>().velocity * reflectRatio, collision.contacts[0].point, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(TroopsImpactSound, this.transform.position);
        }

        if (collision.collider.tag == "Destructible")
        {
            collision.gameObject.GetComponent<IDamageable>().Hit(damage);
            AudioSource.PlayClipAtPoint(TerrainImpactSound, this.transform.position);
        }

        if (collision.collider.tag == "Terrain")
        {
            AudioSource.PlayClipAtPoint(TerrainImpactSound, this.transform.position);
        }

    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }

}
