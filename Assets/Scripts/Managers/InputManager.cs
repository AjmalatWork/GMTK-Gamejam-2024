using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool GetJumpInputDown()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool GetJumpInputUp()
    {
        return Input.GetButtonUp("Jump");
    }

    public bool GetExtendArmInputDown()
    {
        return Input.GetButtonDown("Extend");
    }

    public bool GetExtendArmInputUp()
    {
        return Input.GetButtonUp("Extend");
    }

    public bool GetPowerInputDown()
    {
        return Input.GetButtonDown("Power");
    }
}
