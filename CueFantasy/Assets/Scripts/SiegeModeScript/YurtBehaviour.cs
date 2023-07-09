using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YurtBehaviour : MonoBehaviour, IDamageable, IDestroyable
{
    [SerializeField] private int hp = 500;
    [SerializeField] private int hpLimit = 500;
    [SerializeField] private GameObject BigDustExplosionFX;
    [SerializeField] private AudioClip DestructionSound;
    [SerializeField] private AudioClip ImpactSound;
    [SerializeField] GameObject GameoverMenu;
    [SerializeField] GameObject SiegeBoss;
    private AudioSource audioSource;
    private GameObject HealthBar;
    private HealthBarController healthBarController;
    private SiegeEndGameMenu siegeEndGameMenu;
    private bool isCalled = false;
    private bool isMediumnFired = false;
    private bool isLargeFired = false;

    [Header("FX")]
    [SerializeField] private GameObject MediumFireFX;
    [SerializeField] private GameObject LargeFireFX;
    [SerializeField] private GameObject MediumExplosionFX;
    [SerializeField] private GameObject LargeExplosionFX;

    [Header("Audio")]
    [SerializeField] private AudioClip MediumExplosionSound;
    [SerializeField] private AudioClip LargeExplosionSound;

    void Start()
    {
        HealthBar = GameObject.Find("BossHealthBar");
        healthBarController = HealthBar.GetComponent<HealthBarController>();
        healthBarController.SetMaxValue(hp);
        
    }
    void Update()
    {
        if (!isMediumnFired)
        {
            if (hp <= hpLimit * 0.4)
            {
                GameObject mediumFire = Instantiate(MediumFireFX, this.transform.position, Quaternion.identity);
                mediumFire.transform.parent = gameObject.transform;
                Instantiate(MediumExplosionFX, this.transform.position, Quaternion.identity);
                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(MediumExplosionSound, 1f);
                this.transform.Find("FireLight").gameObject.GetComponent<Light>().intensity = 5;
                isMediumnFired = true;
            }
        }

        if (!isLargeFired)
        {
            if(hp <= hpLimit * 0.2)
            {
                GameObject largeFire = Instantiate(LargeFireFX, this.transform.position, Quaternion.identity);
                largeFire.transform.parent = gameObject.transform;
                Instantiate(LargeExplosionFX, this.transform.position, Quaternion.identity);
                audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(LargeExplosionSound, 1f);
                this.transform.Find("FireLight").gameObject.GetComponent<Light>().intensity = 10;
                isLargeFired = true;
            }
        }

    }

    public void Hit(int damage)
    {
        hp -= damage;
        AudioSource.PlayClipAtPoint(ImpactSound, this.transform.position);
        healthBarController.SetValue(hp);
        if (hp <= 0)
        {
            DestroyIt();
        }
    }

    public void DestroyIt()
    {
        Instantiate(SiegeBoss, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Instantiate(BigDustExplosionFX, this.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(DestructionSound, this.transform.position);
    }

}
