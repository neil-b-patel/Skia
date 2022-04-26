using UnityEngine;

public class ShadowPlayerController : MonoBehaviour
{
    float movementSpeed = 2000f;
    bool isFacingRight = true;

    PlayerManager player;

    Rigidbody rb;
    Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        player = GetComponentInParent<PlayerManager>();
        player.SetMusic(isLightPlayer: false);
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = movement * movementSpeed * Time.deltaTime;

        if (movement.x != 0)
        {
            CheckDirectionToFace(movement.x > 0);
        }
    }

    void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }

    void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
        {
            Turn();
        }
    }
    public void SetDirectionToFace(bool direction)
    {
        isFacingRight = direction;
    }

    public bool GetDirectionToFace()
    {
        return isFacingRight;
    }
}
