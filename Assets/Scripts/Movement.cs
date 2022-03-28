using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2d;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        rb2d.velocity = movement * movementSpeed;

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            if (player.LFoot)
            {
                Debug.Log("JUMPING!!!");
                rb2d.AddForce(new Vector2(0,10), ForceMode2D.Impulse);
            }
        }
    }

    
}
