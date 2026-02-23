
using Unity.Netcode;

using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlMovement : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         movement = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical")).normalized;
    }

    void FixedUpdate()
    {
       moveCharacter(movement);
    }
        void moveCharacter(Vector3 direction)
    {
        rb.linearVelocity = direction * speed;
    }
}
