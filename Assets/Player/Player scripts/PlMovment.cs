
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Mirror.Examples.Basic;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]


public class PlMovement : NetworkBehaviour
{


    [Header("Mesh to rotate")]
    public GameObject mesh;

    [Header("Camera")]
    public GameObject cam;

    [SerializeField]
    private InputActionReference m_moveAction;
    public Vector3 movementDirection { get; private set; }
    private Vector3 movmentInput;

    [SerializeField]
    private float m_smoothTime = 0.1f;


    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    public override void OnStartAuthority()
    {
        cam.SetActive(true);
        GetComponent<PlayerInput>().enabled = true;
        GetComponent<PlayerInputManager>().enabled = true;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.AddComponent<NetworkIdentity>();

    }

    void Update()
    {
        Camera();

        PlayerMovement();
    }


    public void PlayerMovement()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            movmentInput = m_moveAction.action.ReadValue<Vector3>();
            movementDirection = new Vector3(movmentInput.x, 0, movmentInput.z) * speed;

        }
    }

    public void Camera()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            cam.transform.position = transform.position + new Vector3(0, 5, -10);

            float playerVerticalInput = Input.GetAxis("Vertical") * speed;
            float playerHorizontalInput = Input.GetAxis("Horizontal") * speed;

            Vector3 camForward = cam.transform.forward;
            Vector3 camRight = cam.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 forwardRelativeToCamera = camForward * playerVerticalInput;
            Vector3 rightRelativeToCamera = camRight * playerHorizontalInput;

            movementDirection = forwardRelativeToCamera + rightRelativeToCamera;


            // rotate the mesh to the direction of movement
            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

    }

}

