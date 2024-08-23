using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    private PlayerMovement movement;
    private PlayerJump jump;
    private WallJump wallJump;
    private ChangeSize changeSize;
    private bool isDead = false;
    private Rigidbody2D rb;
    private Vector3 originalPosition;

    [SerializeField] private ExtendArm extendArm;
    [SerializeField] private float respawnDelay = 2f;


    void Start()
    {
        movement    = GetComponent<PlayerMovement>();
        jump        = GetComponent<PlayerJump>();
        wallJump    = GetComponent<WallJump>();
        changeSize  = GetComponent<ChangeSize>();
        rb          = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    void Update()
    {
        HandlePlayerInput();              
    }

    void HandlePlayerInput()
    {
        if(!isDead)
        {
            movement.HandleMovement();
            jump.HandleJump();
            wallJump.HandleWallJump();
            extendArm.HandleExtendArm();
            changeSize.HandleChangeSize();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = originalPosition;
        }
    }

    public void Die()
    {
        isDead = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        extendArm.isArmAttached = false;
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        isDead = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.identity;
        RespawnManager.Instance.RespawnPlayer(gameObject);        
    }
}
