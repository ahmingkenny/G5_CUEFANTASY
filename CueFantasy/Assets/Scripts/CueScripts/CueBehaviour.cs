using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBehaviour : MonoBehaviour, IDestroyable
{

    private Animator anim;

    [SerializeField] private Vector3 cameraOffset = new Vector3(0.4f, -0.5f, 0.7f);
    [SerializeField] private Vector3 rotation = new Vector3(45f, -15f, 45f);

    protected GameObject AbilityButton;
    protected AbilityButton abilityButton;
    protected GameObject GameManager;
    protected GameFlow gameFlow;
    protected Attacker attacker;
    protected Defender defender;
    protected GameObject MainCamera;
    protected CueBallFollower cueBallFollower;
    protected AbilityCaster abilityCaster;
    protected GameObject NoticeBoard;
    protected NoticeBoard noticeBoard;
    private AIController aiController;

    [Header("Audio")]
    [SerializeField] private AudioClip DrawSound;

    void Start()
    {
        aiController = GameObject.Find("GameManager").GetComponent<AIController>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cueBallFollower = MainCamera.GetComponent<CueBallFollower>();
        anim = GetComponent<Animator>();
        AdjustPosition();
        if (!CueBallFollower.isFollowing)
        {
            anim.SetBool("isDown", true);
        }
        else if (CueBallFollower.isFollowing)
        {
            TakeCueOut();
        }
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && !aiController.isControlling)
        {
            Charging();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !aiController.isControlling)
        {
            Swing();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && !aiController.isControlling)
        {
            anim.SetTrigger("Dance");
        }
    }

    public void Charging()
    {
        if (!GameObject.Find("Canvas").transform.Find("GameBook").gameObject.activeSelf && !GameObject.Find("Canvas").transform.Find("InternalAffairsPanel").gameObject.activeSelf)
            anim.SetBool("isCharging", true);
    }

    public void Swing()
    {
        if (!GameObject.Find("Canvas").transform.Find("GameBook").gameObject.activeSelf && !GameObject.Find("Canvas").transform.Find("InternalAffairsPanel").gameObject.activeSelf)
        {
            anim.SetBool("isCharging", false);
            anim.SetBool("isSwing", true);
        }
    }

    private void AdjustPosition()
    {
        GameObject cueBall = GameObject.FindGameObjectWithTag("CueBall");
        transform.LookAt(cueBall.transform.position);
        this.transform.Translate(cameraOffset);
        this.transform.Rotate(rotation, Space.World);
    }

    public void TakeCueOut()
    {
        anim.SetBool("isDown", false);
        anim.SetBool("isPutDown", false);
        anim.SetTrigger("TakeOut");
        AudioSource.PlayClipAtPoint(DrawSound, this.transform.position);
    }

    public void PutCueDown()
    {
        anim.SetBool("isPutDown", true);
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }

}
