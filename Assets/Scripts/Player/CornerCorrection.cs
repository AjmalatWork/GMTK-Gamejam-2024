using UnityEngine;

public class CornerCorrection : MonoBehaviour
{
    public Transform TopLeft;    
    public Transform TopRight;
    public Transform BottomRight;
    public LayerMask hitLayer;
    public float cornerJumpCheckDistance;
    public float verticalRayDistance;    
    public float horizontalRayDistance;
    public float cornerJumpOffset;

    private Vector3 LeftInnerPosition;
    private Vector3 RightInnerPosition;
    private Vector3 LeftOuterPosition;
    private Vector3 RightOuterPosition;
    private RaycastHit2D leftOuterHit;
    private RaycastHit2D leftInnerHit;
    private RaycastHit2D rightOuterHit;
    private RaycastHit2D rightInnerHit;

    private float playerDirection;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        GetPlayerDirection();

        if(rb.velocity.y > 0)
        {
            JumpCorrection();
        }        
    }

    private void GetPlayerDirection()
    {
        if (transform.localScale.x > 0)
        {
            playerDirection = 1f;
        }
        else if (transform.localScale.x < 0)
        {
            playerDirection = -1f;
        }
    }

    private void JumpCorrection()
    {
        LeftOuterPosition = TopLeft.position;
        RightOuterPosition = TopRight.position;

        LeftInnerPosition = new(TopLeft.position.x + playerDirection * cornerJumpCheckDistance, TopLeft.position.y);
        RightInnerPosition = new(TopRight.position.x - playerDirection * cornerJumpCheckDistance, TopRight.position.y);

        leftOuterHit = Physics2D.Raycast(LeftOuterPosition, Vector2.up, verticalRayDistance, hitLayer);
        leftInnerHit = Physics2D.Raycast(LeftInnerPosition, Vector2.up, verticalRayDistance, hitLayer);
        rightOuterHit = Physics2D.Raycast(RightOuterPosition, Vector2.up, verticalRayDistance, hitLayer);
        rightInnerHit = Physics2D.Raycast(RightInnerPosition, Vector2.up, verticalRayDistance, hitLayer);

        if (leftOuterHit.collider != null && leftInnerHit.collider == null)
        {
            Vector2 horizontalRayStartPoint = new(LeftInnerPosition.x, LeftInnerPosition.y + verticalRayDistance);
            RaycastHit2D LeftHit = Physics2D.Raycast(horizontalRayStartPoint, playerDirection * Vector2.left, horizontalRayDistance, hitLayer);

            if (LeftHit.collider != null)
            {
                float distanceToMove = (LeftHit.point.x - leftOuterHit.point.x) * playerDirection + cornerJumpOffset;
                transform.position = new Vector3(transform.position.x + playerDirection * distanceToMove, transform.position.y, transform.position.z);
            }
        }
        else if (rightOuterHit.collider != null && rightInnerHit.collider == null)
        {
            Vector2 horizontalRayStartPoint = new(RightInnerPosition.x, RightInnerPosition.y + verticalRayDistance);
            RaycastHit2D RightHit = Physics2D.Raycast(horizontalRayStartPoint, playerDirection * Vector2.right, horizontalRayDistance, hitLayer);

            if (RightHit.collider != null)
            {
                float distanceToMove = (rightOuterHit.point.x - RightHit.point.x) * playerDirection + cornerJumpOffset;
                transform.position = new Vector3(transform.position.x - playerDirection * distanceToMove, transform.position.y, transform.position.z);
            }
        }
    }
}
