using UnityEngine;

public class ExtendArm : MonoBehaviour
{
    [SerializeField] private float maxArmLength;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private GameObject ArmOrigin;
    [SerializeField] private DistanceJoint2D distanceJoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Rigidbody2D playerRB;

    public bool isArmAttached;
    private bool extendPressed;
    private bool extendStarted;

    void Start()
    {
        isArmAttached = false; 

        distanceJoint.enabled = false;

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    public void HandleExtendArm()
    {
        if (InputManager.Instance.GetExtendArmInputDown())
        {
            extendPressed = true;
        }

        if (InputManager.Instance.GetExtendArmInputUp())
        {
            extendPressed = false;
        }

        if (extendPressed)
        {            
            ExtendOneArm();
        }
        else if(extendStarted)
        {
            RetractArm();
        }        
    }

    void ExtendOneArm()
    {
        if (isArmAttached)
        {
            lineRenderer.SetPosition(0, ArmOrigin.transform.position);
            return;
        }

        extendStarted = true;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = transform.position.z;
        Vector3 direction = (mouseWorldPosition - ArmOrigin.transform.position).normalized;

        //Vector2 direction = (Vector2.up + Vector2.right * playerRB.transform.localScale.x).normalized; // 45-degree angle
        RaycastHit2D hit = Physics2D.Raycast(ArmOrigin.transform.position, direction, maxArmLength, grabbableLayer);
        if (hit.collider != null)
        {            
            AttachArm(hit.point);
        }
    }

    void AttachArm(Vector2 hitPoint)
    {
        isArmAttached = true;

        distanceJoint.connectedAnchor = hitPoint;
        distanceJoint.distance = Vector2.Distance(hitPoint, ArmOrigin.transform.position);
        distanceJoint.enabled = true;        

        // Enable the line renderer to visualize the arm
        lineRenderer.SetPosition(0, ArmOrigin.transform.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;

        SetSwingParameters();
    }

    void SetSwingParameters()
    {
        playerRB.AddForce(new Vector2(10.0f, 0.0f), ForceMode2D.Force);
    }

    void ResetSwingParameters()
    {

    }

    void RetractArm()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        extendStarted = false;
        isArmAttached = false;
    }
}
