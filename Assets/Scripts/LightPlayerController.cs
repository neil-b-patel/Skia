//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class LightPlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1500f;
    [SerializeField] float jumpVelocity = 250f;
    [SerializeField] float gravityScale = 10f;

    private bool isJumping;
    private bool onGround;

    private int numFeet = 0;
    private int numJumps = 0;

    //private float vInput;
    //private float hInput;

    private Rigidbody rb;
    private Collider col;

    private Vector3 movement;
    private LayerMask groundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        isJumping |= Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        rb.velocity = movement * movementSpeed * Time.deltaTime;

        onGround = IsGrounded();
        numJumps = onGround ? 0 : numJumps;

        rb.AddForce(Physics.gravity * gravityScale * rb.mass);

        if (isJumping && (onGround || numJumps <= 1))
        {
            if (numFeet == 1 && (numJumps < 1 || onGround)
                || numFeet == 2 && (numJumps < 2 || onGround))
            {
                rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
                numJumps += 1;
            }
        }

        isJumping = false;
    }

    bool IsGrounded()
    {
        Vector3 sphereBottom = new Vector3(col.bounds.center.x, 
                                           col.bounds.min.y, 
                                           col.bounds.center.z);

        bool grounded = Physics.CheckSphere(sphereBottom, 
                                            0.5f, 
                                            groundLayer, 
                                            QueryTriggerInteraction.Ignore);

        return grounded;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("CanBePickedUp"))
        {
            numFeet += 1;
            Debug.Log("Gained a Foot!");
            if (numFeet == 1)
            {
                Debug.Log("Try jumping with Space!");
            }
            Destroy(collider.gameObject);
        }
    }
}
