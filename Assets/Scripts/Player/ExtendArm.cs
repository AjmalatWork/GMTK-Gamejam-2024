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
    public bool extendPressed;
    private bool extendStarted;
    private FallingLedge fallingLedge;
    private RaycastHit2D hit;

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
        
        if(isArmAttached && fallingLedge != null)
        {
            distanceJoint.connectedAnchor = new Vector2(hit.point.x,fallingLedge.transform.position.y);
            lineRenderer.SetPosition(1, new Vector2(hit.point.x, fallingLedge.transform.position.y));
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
        hit = Physics2D.Raycast(ArmOrigin.transform.position, direction, maxArmLength, grabbableLayer);
        if (hit.collider != null)
        {            
            AttachArm(hit);
        }
    }

    void AttachArm(RaycastHit2D hit)
    {
        isArmAttached = true;

        distanceJoint.connectedAnchor = hit.point;
        distanceJoint.distance = Vector2.Distance(hit.point, ArmOrigin.transform.position);
        distanceJoint.enabled = true;        

        // Enable the line renderer to visualize the arm
        lineRenderer.SetPosition(0, ArmOrigin.transform.position);
        lineRenderer.SetPosition(1, hit.point);
        lineRenderer.enabled = true;

        // Is this a falling ledge?
        fallingLedge = hit.collider.gameObject.GetComponentInParent<FallingLedge>();
        if (fallingLedge != null)
        {
            fallingLedge.GrabLedge();
        }
    }

    void RetractArm()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        extendStarted = false;
        isArmAttached = false;
    }
}
