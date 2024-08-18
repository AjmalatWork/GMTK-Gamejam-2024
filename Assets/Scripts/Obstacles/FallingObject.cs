using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float deactiveDelay = 1f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        DeactivateObject();
    }

    private void Update()
    {
        if (rb.velocity.y < -maxVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.Die();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke(nameof(DeactivateObject), deactiveDelay);
        }

    }
}