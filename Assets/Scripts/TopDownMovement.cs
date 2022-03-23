using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpd = 1.0f;
    private Vector2 movement;
    private Rigidbody2D rigidBody;
    //private Collider2D tileMapCollider;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //tileMapCollider = GameObject.Find("Ground").GetComponent<Collider2D>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rigidBody.velocity = movement * movementSpd;
    }

    // NEED HELP WITH COLLIDERS / TRIGGERS TO STOP PLAYER FROM EXITING MAP

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Hello");
    //    if (other.gameObject.name == "Ground")
    //    {
    //        movement = Vector2.zero;
    //    }
    //}
}
