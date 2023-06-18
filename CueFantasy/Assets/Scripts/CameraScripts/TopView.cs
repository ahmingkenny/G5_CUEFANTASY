using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopView : MonoBehaviour
{
    [SerializeField] private float camSpeed = 4f;
    [SerializeField] private float camHeight = 4f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public static bool isViewing = false;
    public static bool isSelecting = false;

    private int layer_mask;

    [Header("Reference")]
    private GameObject CueBall;
    private CueBallBehaviour cueBallBehaviour;
    private AIController aiController;

    void Awake()
    {
        Reset();
    }

    void Start()
    {
        CueBall = GameObject.FindGameObjectWithTag("CueBall");
        cueBallBehaviour = CueBall.GetComponent<CueBallBehaviour>();
        GameObject worldCenter = GameObject.FindGameObjectWithTag("WorldCenter");
        targetPosition = new Vector3(worldCenter.transform.position.x, camHeight, worldCenter.transform.position.z);
        aiController = GameObject.Find("GameManager").GetComponent<AIController>();

        layer_mask = LayerMask.GetMask("InvisibleWall", "Ability");
    }

    void Update()
    {
        if (!CueBallFollower.isFollowing && !isSelecting && !PerspectiveView.isAiming && !BallShooter.isShoot && !Input.GetKey(KeyCode.Tab) && CueBall.GetComponent<Rigidbody>().velocity.y == 0)
        {

            if (Input.GetKeyUp(KeyCode.F) && !PerspectiveView.isLerping && !PerspectiveView.isSelectLerp && !aiController.isControlling)
            {
                isViewing = isViewing == false ? true : false;
                PerspectiveView.isLerping = isViewing == false ? true : false; //turn on isLerping to return to the original camera position before roaming, and recording of camera position will be ture after lerping in PerspectiveView
                
            }

        }

        if (isViewing)
        {
            GameObject worldCenter = GameObject.FindGameObjectWithTag("WorldCenter");
            targetRotation = Quaternion.LookRotation(worldCenter.transform.position - targetPosition);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, camSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, camSpeed * Time.deltaTime);

        }

        if (isSelecting == true && CueBall.GetComponent<Rigidbody>().velocity.z == 0)
        {

            isViewing = true;

            if (Input.GetButtonUp("Fire1"))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                ChoosePosition(ray);

            }

        }

    }

    public void ChoosePosition(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f, ~layer_mask))
        {

            if (hit.collider.CompareTag("Terrain"))
            {
                cueBallBehaviour.SetBallPosition(hit.point);
                isSelecting = false;
                isViewing = false;
            }

        }
    }

    public void ChoosePosition(Vector3 targetPosition)
    {
            if (isSelecting)
            {
                cueBallBehaviour.SetBallPosition(targetPosition);
            }
                isSelecting = false;
                isViewing = false;

    }

    public void Reset()
    {
        isViewing = false;
        isSelecting = false;
    }

}
