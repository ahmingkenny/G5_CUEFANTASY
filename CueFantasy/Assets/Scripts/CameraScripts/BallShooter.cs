using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    private float range = 3f;
    [SerializeField] private float power = 0.3f;
    bool turnoffFollowing = false;

    private GameObject MainCamera;
    private AbilityCaster abilityCaster;

    private GameObject GameManager;
    private SoundPlayer soundPlayer;

    [Header("Status")]
    public static bool isTopSpin = false;
    public static bool isBackSpin = false;
    public static bool isLeftSpin = false;
    public static bool isRightSpin = false;

    public static bool isShooting = false;
    public static bool isShoot = false;

    [Header("Game Reference")]
    private AIController aiController;

    [Header("UI Reference")]
    private Image TopSpinMap;
    private Image BackSpinMap;
    private Image LeftSpinMap;
    private Image RightSpinMap;
    private Image NormalMap;
    private Slider powerSlider;
    private int layer_mask;

    [Header("FX")]
    [SerializeField] private GameObject SparksFX;

    [Header("Audio")]
    [SerializeField] private AudioClip HitSound;
    [SerializeField] private AudioClip SwingSound;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        soundPlayer = GameManager.GetComponent<SoundPlayer>();

        TopSpinMap = GameObject.FindGameObjectWithTag("CueBallTopSpin").GetComponent<Image>();
        BackSpinMap = GameObject.FindGameObjectWithTag("CueBallBackSpin").GetComponent<Image>();
        LeftSpinMap = GameObject.FindGameObjectWithTag("CueBallLeftSpin").GetComponent<Image>();
        RightSpinMap = GameObject.FindGameObjectWithTag("CueBallRightSpin").GetComponent<Image>();
        NormalMap = GameObject.FindGameObjectWithTag("CueBallNormal").GetComponent<Image>();

        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<Slider>();

        MainCamera = GameObject.Find("Main Camera");

        layer_mask = LayerMask.GetMask("CueBall");

        aiController = GameObject.Find("GameManager").GetComponent<AIController>();

    }

    private void FixedUpdate()
    {
        if (turnoffFollowing)
        {
            CueBallFollower.isFollowing = false;
            turnoffFollowing = false;
        }
    }
    void Update()
    {

        if (CueBallFollower.isFollowing && !aiController.isControlling)
        {

            if (Input.GetKeyUp(KeyCode.Q))
            {
                SwitchToTopSpin();
                soundPlayer.PlayNormalButtonSound();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                SwitchToBackSpin();
                soundPlayer.PlayNormalButtonSound();
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                SwitchToLeftSpin();
                soundPlayer.PlayNormalButtonSound();
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                SwitchToRightSpin();
                soundPlayer.PlayNormalButtonSound();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                isShooting = true;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                isShooting = false;
            }

            if (power != powerSlider.minValue && Input.GetButtonUp("Fire1"))
            {
                Shoot();
            }
            else if (power == powerSlider.minValue && Input.GetButtonUp("Fire1"))
            {
                AudioSource.PlayClipAtPoint(SwingSound, this.transform.position);
            }
        }
    }

    public void AdjustPower(float value)
    {
        power = value;
    }

    public void Shoot()
    {
        abilityCaster = MainCamera.GetComponent<AbilityCaster>();

        Ray ray = new Ray(Quaternion.Euler(0, 0, 0f) * this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, layer_mask))
        {

            if (hit.collider.CompareTag("CueBall") && hit.collider.GetComponent<Rigidbody>().velocity.z == 0 && isShoot == false)
            {
                hit.rigidbody.AddForceAtPosition(ray.direction * power, hit.point, ForceMode.Impulse);
                AudioSource.PlayClipAtPoint(SwingSound, this.transform.position);
                AudioSource.PlayClipAtPoint(HitSound, this.transform.position);
                Instantiate(SparksFX, hit.point, Quaternion.identity);
                isShoot = true;
                PerspectiveView.isLerping = true;
                turnoffFollowing = true;
                GameObject Cue = GameObject.FindGameObjectWithTag("Cue");
                Cue.GetComponent<CueBehaviour>().PutCueDown();
                abilityCaster.isCasting = false;
            }
        }
        powerSlider.value = powerSlider.minValue;
    }

    private void SwitchToTopSpin()
    {
        if (isTopSpin == false)
        {
            isTopSpin = true;
            isBackSpin = false;
            isLeftSpin = false;
            isRightSpin = false;
            TopSpinMap.enabled = true;
            BackSpinMap.enabled = false;
            LeftSpinMap.enabled = false;
            RightSpinMap.enabled = false;
            NormalMap.enabled = false;
        }
        else if (isTopSpin)
        {
            isTopSpin = false;
            NormalMap.enabled = true;
            TopSpinMap.enabled = false;
        }
    }

    private void SwitchToBackSpin()
    {
        if (isBackSpin == false)
        {
            isBackSpin = true;
            isTopSpin = false;
            isLeftSpin = false;
            isRightSpin = false;
            BackSpinMap.enabled = true;
            TopSpinMap.enabled = false;
            LeftSpinMap.enabled = false;
            RightSpinMap.enabled = false;
            NormalMap.enabled = false;
        }
        else if (isBackSpin)
        {
            isBackSpin = false;
            NormalMap.enabled = true;
            BackSpinMap.enabled = false;
        }
    }

    private void SwitchToLeftSpin()
    {
        if (isLeftSpin == false)
        {
            isLeftSpin = true;
            isTopSpin = false;
            isBackSpin = false;
            isRightSpin = false;
            LeftSpinMap.enabled = true;
            TopSpinMap.enabled = false;
            BackSpinMap.enabled = false;
            RightSpinMap.enabled = false;
            NormalMap.enabled = false;
        }
        else if (isLeftSpin)
        {
            isLeftSpin = false;
            NormalMap.enabled = true;
            LeftSpinMap.enabled = false;
        }
    }

    private void SwitchToRightSpin()
    {
        if (isRightSpin == false)
        {
            isRightSpin = true;
            isTopSpin = false;
            isBackSpin = false;
            isLeftSpin = false;
            RightSpinMap.enabled = true;
            TopSpinMap.enabled = false;
            BackSpinMap.enabled = false;
            LeftSpinMap.enabled = false;
            NormalMap.enabled = false;
        }
        else if (isRightSpin)
        {
            isRightSpin = false;
            NormalMap.enabled = true;
            RightSpinMap.enabled = false;
        }
    }

    public void Reset()
    {
        isTopSpin = false;
        isBackSpin = false;
        isLeftSpin = false;
        isRightSpin = false;
        isShooting = false;
        isShoot = false;
}

}
