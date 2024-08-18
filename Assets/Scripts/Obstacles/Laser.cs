using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected float laserDistance = 100;
    public Transform laserStartPoint;
    public Vector2 laserDirection;
    public bool isLaserOn = true;
    public enum LaserMode { Button, Timed }
    public LaserMode mode;

    public float laserSwitchTimer = 2f;

    LineRenderer lineRenderer;
    BoxCollider2D laserCollider;
    LayerMask ignoreLayer;
    readonly float laserOffset = 1f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        laserCollider = GetComponent<BoxCollider2D>();

        int allLayers = ~0;
        int excludedLayers = allLayers & ~(1 << 6) & ~(1 << gameObject.layer);
        ignoreLayer = excludedLayers;

        if (mode == LaserMode.Timed)
        {
            InvokeRepeating(nameof(InvokeSwitch), laserSwitchTimer, laserSwitchTimer);
        }
    }

    void Update()
    {       
        if (isLaserOn)
        {
            ShootLaser(laserStartPoint.position, laserDirection);
        }
    }

    void ShootLaser(Vector3 start, Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(start, direction, laserDistance, ignoreLayer);
        if (hit.collider != null)
        {
            Draw2DRay(start, hit.point);
        }
        else
        {
            Draw2DRay(start, start + direction * laserDistance);
        }
    }

    void Draw2DRay(Vector2 startpos, Vector2 endpos)
    {
        // Set start and end point
        lineRenderer.SetPosition(0, startpos);
        lineRenderer.SetPosition(1, endpos);

        // Set collider properties
        Vector2 colliderSize = new Vector2((endpos - startpos).magnitude + laserOffset, lineRenderer.startWidth);
        Vector2 colliderOffset = new Vector2((colliderSize.x / 2 + transform.localScale.x / 2), 0);
        laserCollider.size = colliderSize;
        laserCollider.offset = colliderOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player touches the laser, you lose!
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.Die();
        }
    }

    public void SwitchLaser(bool switchLaser)
    {
        lineRenderer.enabled = switchLaser;
        laserCollider.enabled = switchLaser;
        isLaserOn = switchLaser;
    }

    void InvokeSwitch()
    {
        SwitchLaser(!isLaserOn);
    }
}
