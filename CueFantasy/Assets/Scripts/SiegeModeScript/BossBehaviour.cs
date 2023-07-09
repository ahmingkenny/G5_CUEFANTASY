using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 200;
    [SerializeField] private int hpLimit = 200;
    [SerializeField] private float lerpSpeed = 2f;
    [SerializeField] private float extraTime = 90f;
    private GameObject HealthBar;
    private HealthBarController healthBarController;
    private SiegeEndGameMenu siegeEndGameMenu;
    private bool isCalled = false;
    private bool isArrived = false;
    private string bossName = "成吉思汗的夙願";

    [SerializeField] private GameObject BossSlash;
    [SerializeField] private GameObject SlashRangeIndicator;
    private GameObject indicator;
    [SerializeField] private bool isSlashOnCoolDown = true;
    [SerializeField] private float slashCastTime = 3f;
    private float slashInterval;
    [SerializeField] private float slashAboveGround = 0.3f;
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 7f;
    private Vector3 slashStartPosition;
    private Vector3 targetPosition;

    [Header("Audio")]
    [SerializeField] private AudioClip DeathSound;
    [SerializeField] private AudioClip ImpactSound;
    [SerializeField] private AudioClip HurtSound;
    [SerializeField] private AudioClip SpawnRing;
    [SerializeField] private AudioClip SpawnSound;
    [SerializeField] private AudioClip StepSound;
    [SerializeField] private AudioClip SlashSound;

    [Header("FX")]
    [SerializeField] private GameObject DebrisWithFire;

    void Start()
    {
        HealthBar = GameObject.Find("BossHealthBar");
        healthBarController = HealthBar.GetComponent<HealthBarController>();
        healthBarController.SetMaxValue(hp);
        healthBarController.SetValue(hp);
        HealthBar.transform.Find("Text").gameObject.GetComponent<Text>().text = bossName;
        GameObject.Find("Timer").GetComponent<CountDownTimer>().AddTime(extraTime);
        GetComponent<AudioSource>().PlayOneShot(SpawnRing, 1f);
        GetComponent<AudioSource>().PlayOneShot(StepSound, 1f);

        this.transform.position = GameObject.Find("BossStartPosition").transform.position;
        GetComponent<Animator>().SetBool("isRunning", true);

        Instantiate(DebrisWithFire, GameObject.Find("BossTargetPosition").transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isArrived)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, GameObject.Find("BossTargetPosition").transform.position, lerpSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(this.transform.position, GameObject.Find("BossTargetPosition").transform.position) < 0.3f && !isArrived)
        {
            GetComponent<Animator>().SetBool("isRunning", false);
            GameObject.Find("GameManager").GetComponent<SiegeModeAIController>().StartPhase2();
            isArrived = true;
            CoolDownDone();
        }

        if (!isSlashOnCoolDown)
        {
            Invoke("CallSlash", slashCastTime);
            GetComponent<Animator>().SetBool("isCharging", true);
            GetComponent<AudioSource>().PlayOneShot(SpawnSound, 1.5f);
            targetPosition = GameObject.Find("BossTargetPosition").transform.position;
            Vector3 indicatorPosition = targetPosition;
            indicatorPosition.y -= 0.615f;
            indicator = Instantiate(SlashRangeIndicator, indicatorPosition, Quaternion.identity);
            isSlashOnCoolDown = true;
        }

    }

    public void Hit(int damage)
    {
        hp -= damage;
        GetComponent<Animator>().SetTrigger("Hurt");
        AudioSource.PlayClipAtPoint(ImpactSound, this.transform.position);
        GetComponent<AudioSource>().PlayOneShot(HurtSound, 1.5f);
        healthBarController.SetValue(hp);
        if (hp <= 0)
        {
            if (!isCalled)
            {
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<AudioSource>().PlayOneShot(DeathSound, 1.25f);
                GameObject GameoverMenu = GameObject.Find("Canvas").transform.Find("SiegeEndGameMenu").gameObject;
                siegeEndGameMenu = GameoverMenu.GetComponent<SiegeEndGameMenu>();
                GameoverMenu.SetActive(true);
                siegeEndGameMenu.ShowWin();
                isCalled = true;
            }
        }
    }
    private void CallSlash()
    {
        slashInterval = Random.Range(minInterval, maxInterval);
        Invoke("CoolDownDone", slashInterval);
        Destroy(indicator);
        GetComponent<Animator>().SetBool("isCharging", false);
        GetComponent<Animator>().SetTrigger("Attack");
        slashStartPosition = GameObject.Find("BossTargetPosition").transform.position;
        slashStartPosition.y += 0.3f;
        GameObject bossSlash = Instantiate(BossSlash, slashStartPosition, Quaternion.identity);
        bossSlash.transform.Rotate(0f, 0.0f, 15f, Space.World);
        GetComponent<AudioSource>().PlayOneShot(SlashSound, 1f);
    }
    private void CoolDownDone()
    {
        isSlashOnCoolDown = false;
    }

}
