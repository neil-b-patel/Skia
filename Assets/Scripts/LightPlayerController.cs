using UnityEngine;

public class LightPlayerController : MonoBehaviour
{
    // PLAYER DATA => SHOULD ALL EVENTUALLY BE INHERITED FROM `PlayerData` SCRIPT
    //private PlayerData data;
    private int numFeet = 0;

    // RUN VARS
    private float moveInput;
    private float moveSpeed = 15f;
    private float acceleration = 13f;
    private float decceleration = 16f;
    private float velocityPower = 0.9f;
    private float defaultFriction = 0.2f;

    // JUMP VARS
    private float jumpForce = 15f;
    private float jumpCutMultiplier = 0.5f;
    private float jumpCoyoteTime = 0.15f;
    private float jumpBufferTime = 0.1f;
    private float lastGroundedTime = 0f;
    private float lastJumpTime = 0f;
    private bool isJumping = false;
    private int numJumps = 0;

    // UNITY VARS
    private Rigidbody rb;
    private Collider col;
    private LayerMask groundLayer;


    void Start()
    {
        // LOAD UNITY VARS
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // INPUT CHECKS
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnJump();
        }

        // MAINTAIN TIMERS
        Timers();
        
        // PHYSICS CHECKS
        if (!isJumping && IsGrounded())
        {
            lastGroundedTime = jumpCoyoteTime;
            numJumps = 0;
        }

        if (isJumping && IsFalling())
        {
            isJumping = false;
        }

        // JUMP CHECKS
        if ((CanJump() || CanDoubleJump()) && lastJumpTime > 0)
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            OnJumpUp();
        }
    }

    void FixedUpdate()
    {
        Run();
        AddFriction();
    }


    #region MOVEMENT
    void Run()
    {
        // ACCEL/DECCEL
        float targetSpeed = moveInput * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velocityPower) * Mathf.Sign(speedDiff);

        // LERP => PREVENT RUN FROM IMMEDIATELY SLOWING PLAYER DOWN IN SITUATIONS (E.G. WALL JUMP, DASH) 
        //movement = Mathf.Lerp(rb.velocity.x, movement, 5f);

        rb.AddForce(movement * Vector3.right);
    }

    void AddFriction()
    {
        if (Mathf.Abs(moveInput) < 0.01f)
        {
            float friction = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(defaultFriction));
            friction *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector3.right * -friction, ForceMode.Impulse);
        }
    }

    void Jump()
    {
        lastJumpTime = 0f;
        lastGroundedTime = 0f;
        numJumps += 1;
        isJumping = true;

        float adjustedJumpForce = jumpForce;
        if (IsFalling())
        {
            adjustedJumpForce -= rb.velocity.y;
        }

        rb.AddForce(Vector2.up * adjustedJumpForce, ForceMode.Impulse);
    }
    #endregion


    #region HANDLERS
    // PRESSED JUMP BUTTON
    void OnJump()
    {
        lastJumpTime = jumpBufferTime;
    }

    // RELEASED JUMP BUTTON
    void OnJumpUp()
    {
        if (CanJumpCut())
        {
            JumpCut();
        }
    }

    // CONTROLS JUMP HEIGHT DEPENDING ON HOW LONG JUMP IS HELD
    void JumpCut()
    {
        rb.AddForce(Vector3.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode.Impulse);
    }

    // MAINTAINS TIMERS
    void Timers()
    {
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
    }
    #endregion


    #region CHECKS
    bool IsGrounded()
    {
        Vector3 sphereBottom = new Vector3(col.bounds.center.x,
                                           col.bounds.min.y,
                                           col.bounds.center.z);

        bool grounded = Physics.CheckSphere(sphereBottom,
                                            0.1f,
                                            groundLayer,
                                            QueryTriggerInteraction.Ignore);

        return grounded;
    }

    bool IsFalling()
    {
        return rb.velocity.y < 0;
    }

    bool IsRising()
    {
        return rb.velocity.y > 0;
    }

    bool CanJump()
    {
        return (numFeet > 0) && (lastGroundedTime > 0);
    }

    bool CanJumpCut()
    {
        return isJumping && IsRising();
    }

    bool CanDoubleJump()
    {
        return (numFeet > 1) && (numJumps < 2);
    }
    #endregion CHECKS


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
