using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTroopsBehaviour : MonoBehaviour
{
    [SerializeField] private float reflectRatio = 0.2f;
    [SerializeField] private float directionalRatio = 0.6f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float force = 10f;

    private GameObject Flash;

    [SerializeField] private GameObject DustExplosionFX;

    [Header("Audio")]
    [SerializeField] private AudioClip WhipSound;
    [SerializeField] private AudioClip HorseSound;
    [SerializeField] private AudioClip TroopsImpactSound;

    // Start is called before the first frame update
    void Start()
    {
        Flash = GameObject.Find("Flash");
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
            Instantiate(DustExplosionFX, collision.contacts[0].point, Quaternion.identity);
            AudioSource.PlayClipAtPoint(WhipSound, this.transform.position);
            AudioSource.PlayClipAtPoint(HorseSound, this.transform.position);
            AudioSource.PlayClipAtPoint(TroopsImpactSound, this.transform.position);
        }

        if (collision.collider.tag == "JadeBall")
        {
            collision.gameObject.GetComponent<IDamageable>().Hit(damage);
            Flash.GetComponent<FlashingController>().WakeUp();
            AudioSource.PlayClipAtPoint(TroopsImpactSound, this.transform.position);
        }

    }

    public void Charge(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }

}
