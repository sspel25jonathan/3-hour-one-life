using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Collections;
using Mirror.Examples.Basic;

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

    private Vector3 movementInput;
    public float speed = 5f;



    public Vector3 movementDirection { get; private set; }

    // Runs when the local player gains control
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        cam.SetActive(true);
        rb = GetComponent<Rigidbody>();
        playerInput.enabled = true;
    }

    void Awake()
    {

        playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = false;
        moveAction = playerInput.actions["Move"];

        if (!isLocalPlayer)
        {
            cam.SetActive(false);
            moveAction.Disable();
        }
    }
    void Start()
    {
        if (!isLocalPlayer)
        {
            cam.SetActive(false);

        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        CameraFollow();
        UpdateMeshRotation();
        Vector3 movmentInput = playerInput.actions["Move"].ReadValue<Vector3>();

    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        ReadInput();
        PlayerMovement();
    }

    void ReadInput()
    {
        movementInput = moveAction.ReadValue<Vector3>();
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

    }

    void CameraFollow()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, 5, -10);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * 5f);
        cam.transform.LookAt(transform);
    }

    void UpdateMeshRotation()
    {
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
}