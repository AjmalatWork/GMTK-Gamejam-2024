using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    public float sizeScale = 5f;    
    public float speedMultilpier = 2f;    
    public float jumpMultiplier = 2f;    
    public float handReachMultiplier = 2f; 
    public ExtendArm arm;
    private enum PlayerSize { Normal, Tiny }
    private PlayerSize playerSize;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    void Start()
    {
        playerSize = PlayerSize.Normal;
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
    }

    public void HandleChangeSize()
    {      
        if (InputManager.Instance.GetPowerInputDown())
        {
            ScaleSize();
        }
    }

    void ScaleSize()
    {
        // Change to Tiny
        if(playerSize == PlayerSize.Normal)
        {
            playerSize = PlayerSize.Tiny;
            transform.localScale = new(transform.localScale.x / sizeScale, transform.localScale.y / sizeScale);
            playerMovement.moveSpeed *= speedMultilpier;
            playerJump.maxJumpHeight /= jumpMultiplier;
            arm.maxArmLength /= handReachMultiplier;
        }

        // Change to Normal
        else
        {
            playerSize = PlayerSize.Normal;
            transform.localScale = new(transform.localScale.x * sizeScale, transform.localScale.y * sizeScale);
            playerMovement.moveSpeed /= speedMultilpier;
            playerJump.maxJumpHeight *= jumpMultiplier;
            arm.maxArmLength *= handReachMultiplier;
        }
    }
}
