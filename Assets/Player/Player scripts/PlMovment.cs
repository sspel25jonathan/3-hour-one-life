
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]


public class PlMovement : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Vector3 movementDirection;

    [Header("Mesh to rotate")]
    public GameObject mesh;

    [Header("Camera")]
    public GameObject cam;

    [SerializeField] 
    private PlayerInputManager playerInputManager;
    


    public override void OnStartAuthority()
    {
        cam.SetActive(true);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.AddComponent<NetworkIdentity>();

    }

    void Update()
    {

        Camera();


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

            rb.linearVelocity = new Vector3(
                movementDirection.x,
                rb.linearVelocity.y,
                movementDirection.z
            );


            // rotate the mesh to the direction of movement
            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

    }

}

