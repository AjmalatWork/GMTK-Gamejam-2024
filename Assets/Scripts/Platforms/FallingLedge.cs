using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class FallingLedge : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float fallingTime = 1f;
    [SerializeField] private float resetDelay = 1f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private ExtendArm arm;

    private Rigidbody2D rb;
    private Vector3 originalPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (rb.velocity.y < -maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocity);
        }
    }

    public void GrabLedge()
    {
        Invoke(nameof(Fall), fallDelay);
    }

    private void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke(nameof(Reset), fallingTime);
    }

    private void Reset()
    {
        gameObject.SetActive(false);
        arm.extendPressed = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = originalPosition;
        Invoke(nameof(Activate), resetDelay);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
}
