using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlMovement : NetworkBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody rb;

    [Header("Mesh to rotate")]
    public GameObject mesh;

    [Header("Camera")]
    public GameObject cam;

    private Vector2 movementInput;
    public float speed = 5f;

    [SyncVar] 
    private Vector3 syncedVelocity;

    public Vector3 movementDirection { get; private set; }

    // Runs when the local player gains control
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        cam.SetActive(true);

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        
    }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        CameraFollow();
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            ReadInput();
            PlayerMovement();

            // Sync velocity to server
            CmdSendVelocity(rb.linearVelocity);
        }
        else
        {
            // Remote players use synced velocity
            rb.linearVelocity = syncedVelocity;
        }
    }

    void ReadInput()
    {
        movementInput = moveAction.ReadValue<Vector2>();
    }

    void PlayerMovement()
    {
        // Camera-relative movement
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 forwardRelative = camForward * movementInput.y;
        Vector3 rightRelative = camRight * movementInput.x;

        movementDirection = (forwardRelative + rightRelative) * speed;

        rb.linearVelocity = new Vector3(
            movementDirection.x,
            rb.linearVelocity.y,
            movementDirection.z
        );

        // Rotate mesh toward movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            mesh.transform.rotation = Quaternion.Slerp(
                mesh.transform.rotation,
                targetRotation,
                Time.deltaTime * 10f
            );
        }
    }

    void CameraFollow()
    {
        cam.transform.position = transform.position + new Vector3(0, 5, -10);
        cam.transform.LookAt(transform);
    }

    [Command]
    void CmdSendVelocity(Vector3 vel)
    {
        syncedVelocity = vel;
    }
}