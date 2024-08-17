using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    private PlayerMovement movement;
    private PlayerJump jump;
    private WallJump wallJump;
    [SerializeField] private ExtendArm extendArm;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        jump     = GetComponent<PlayerJump>();
        wallJump = GetComponent<WallJump>();
    }

    void Update()
    {
        HandlePlayerInput();              
    }

    void HandlePlayerInput()
    {
        movement.HandleMovement();
        jump.HandleJump();
        wallJump.HandleWallJump();
        extendArm.HandleExtendArm();
    }
}
