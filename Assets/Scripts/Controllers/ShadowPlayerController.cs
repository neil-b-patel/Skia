using UnityEngine;

public class ShadowPlayerController : MonoBehaviour
{
    float movementSpeed = 1750f;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.enabled = false;
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = movement * movementSpeed * Time.deltaTime;
    }
}
