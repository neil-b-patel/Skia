//using System.Collctions;
using UnityEngine;

public class ShadowBehavior : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1500f;

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

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = movement * movementSpeed * Time.deltaTime;
    }

}
