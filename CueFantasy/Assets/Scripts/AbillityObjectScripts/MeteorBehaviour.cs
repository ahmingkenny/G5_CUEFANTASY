using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
    [SerializeField] private float power = 0.3f;
    [SerializeField] private int damage = 20;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private GameObject DustExplosion;
    [SerializeField] private AudioClip ExplosionSound;
    private bool isExploded = false;
    private CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isExploded)
        {
            if (this.transform.position.y <= 0)
            {
                Instantiate(DustExplosion, this.transform.position, Quaternion.identity);
                Instantiate(FireBall, this.transform.position, Quaternion.identity);
                //AudioSource audioSource = GetComponent<AudioSource>();
                //audioSource.PlayOneShot(ExplosionSound, 1f);
                AudioSource.PlayClipAtPoint(ExplosionSound, this.transform.position);

                cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
                StartCoroutine(cameraShake.Shake(0.35f, 0.3f));

                isExploded = true;
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "CueBall" || other.tag == "AttackerBall" || other.tag == "DefenderBall" || other.tag == "JadeBall")
        {
                Vector3 direction = other.ClosestPointOnBounds(transform.position) - this.transform.position;
                other.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * power, other.ClosestPointOnBounds(transform.position), ForceMode.Impulse);
        }

        if (other.tag == "JadeBall")
        {
            other.GetComponent<IDamageable>().Hit(damage);
        }

    }

}
