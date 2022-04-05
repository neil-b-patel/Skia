//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ShadowBehavior : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1500f;
    [SerializeField] float jumpVelocity = 250f;
    [SerializeField] float gravityScale = 10f;

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
        
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = movement * movementSpeed * Time.deltaTime;

    }


    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.CompareTag("CanBePickedUp"))
    //    {
    //        numFeet += 1;
    //        Debug.Log("Gained a Foot!");
    //        if (numFeet == 1)
    //        {
    //            Debug.Log("Try jumping with Space!");
    //        }
    //        Destroy(collider.gameObject);
    //    }
    //}
}
