using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraControll : NetworkBehaviour
{
    public PlayerInput playerInput;
    private InputAction lockMouse;
    private InputAction rightMouse;
    public Camera playerCamera;

    public bool lockMouseToCenter;

    public bool holdingCameraTurn;  



    void Start()
    {
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
            return;
        }

        lockMouse = playerInput.actions["LockMouse"];
        rightMouse = playerInput.actions["CameraTurn"];
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        LockMouseCenter();
    }

    private void LockMouseCenter()
    {
        if (lockMouse.triggered)
        {
            lockMouseToCenter = !lockMouseToCenter;

            if (lockMouseToCenter)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }

    private void AbleToTurnCamera()
    {
        if (rightMouse.IsPressed())
        {
            holdingCameraTurn = true;
        }
        else
        {
            holdingCameraTurn = false;
        }
    }

    
}
