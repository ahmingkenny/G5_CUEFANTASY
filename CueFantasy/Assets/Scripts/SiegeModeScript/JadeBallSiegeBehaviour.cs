using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeBallSiegeBehaviour : MonoBehaviour, IDamageable, IDestroyable
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int hpLimit = 100;
    [SerializeField] private GameObject SmallDustExplosionFX;
    [SerializeField] private AudioClip KingDeathSound;
    [SerializeField] private AudioClip ImpactSound;
    [SerializeField] GameObject GameoverMenu;
    private AudioSource audioSource;
    private GameObject HealthBar;
    private HealthBarController healthBarController;
    private SiegeEndGameMenu siegeEndGameMenu;
    private bool isCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GameObject.Find("KingHealthBar");
        healthBarController = HealthBar.GetComponent<HealthBarController>();
        healthBarController.SetMaxValue(hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit(int damage)
    {
        hp -= damage;
        AudioSource.PlayClipAtPoint(ImpactSound, this.transform.position);
        healthBarController.SetValue(hp);
        if (hp <= 0)
        {
            DestroyIt();
            if (!isCalled)
            {
                siegeEndGameMenu = GameoverMenu.GetComponent<SiegeEndGameMenu>();
                GameoverMenu.SetActive(true);
                siegeEndGameMenu.ShowLose();
                isCalled = true;
            }
        }
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
        Instantiate(SmallDustExplosionFX, this.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(KingDeathSound, this.transform.position);
    }

}
