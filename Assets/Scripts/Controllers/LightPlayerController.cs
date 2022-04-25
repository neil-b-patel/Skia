using UnityEngine;

public class LightPlayerController : MonoBehaviour
{
    #region ANIMATION STATES
    Animator animator;
    string animationState = "AnimationState";
    enum CharStates 
    {   
        noLegIdle = 1,
        rolling = 2,
        oneLegIdle = 3,
        oneLegWalk = 4,
        oneLegHop = 5,
        twoLegIdle = 6,
        twoLegWalk = 7,
        twoLegHop = 8,
        twoLegCrouch = 9
    }

    #endregion

    #region RUN VARS
    private float moveInput;
    private float jumpInput;
    private float moveSpeed = 25f;
    private float acceleration = 20f;
    private float decceleration = 25f;
    private float velocityPower = 0.9f;
    private float defaultFriction = 0.2f;
    private bool isFacingRight = true;
    #endregion


    #region JUMP VARS
    private float jumpForce = 30f;
    private float jumpCutMultiplier = 0.5f;
    private float jumpCoyoteTime = 0.15f;
    private float jumpBufferTime = 0.1f;
    private float lastGroundedTime = 0f;
    private float lastJumpTime = 0f;
    private bool isJumping = false;
    private int numJumps = 0;
    #endregion


    #region UNITY VARS
    private PlayerManager player;
    private ProgressManager progressManager;
    private Rigidbody rb;
    private Collider col;
    private LayerMask groundLayer;
    #endregion


    void Start()
    {
        #region LOAD UNITY VARS
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerManager>();
        progressManager = FindObjectOfType<ProgressManager>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        groundLayer = LayerMask.GetMask("Ground");
        #endregion

        player.SetPosition(Vector3.zero);
    }

    void Update()
    {
        #region INPUT CHECKS
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnJump();
        }
        #endregion

        #region TIMERS
        Timers();
        #endregion

        #region PHYSICS CHECKS
        if (!isJumping && IsGrounded())
        {
            lastGroundedTime = jumpCoyoteTime;
            numJumps = 0;
        }

        if (isJumping && IsFalling())
        {
            isJumping = false;
        }
        #endregion

        #region JUMP CHECKS
        if ((CanJump() || CanDoubleJump()) && lastJumpTime > 0)
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            OnJumpUp();
        }
        #endregion

        #region UPDATE ANIMATION STATE
        UpdateState();
        #endregion
    }

    void FixedUpdate()
    {
        Run();
        AddFriction();
    }


    #region MOVEMENT METHODS
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

        if (moveInput != 0)
        {
            CheckDirectionToFace(moveInput > 0);
        }
    }

    void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
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


    #region PHYSIC HANDLERS
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

    // UPDATE STATE FOR LIGHT CHARACTER CONTROLLER
    private void UpdateState()
    {
        if(CanJump() == false && CanDoubleJump() == false) {
            if (moveInput > 0 || moveInput < 0) {
                animator.SetInteger(animationState, (int)CharStates.rolling);
            } else {
                animator.SetInteger(animationState, (int)CharStates.noLegIdle);
            }
        } else if (CanJump() == true && CanDoubleJump() == false) {
            // need to add crouch
            if (moveInput > 0 || moveInput < 0) {
                animator.SetInteger(animationState, (int)CharStates.oneLegWalk);
            } else if (jumpInput > 0) {
                animator.SetInteger(animationState, (int)CharStates.oneLegHop); 
            } else {
                animator.SetInteger(animationState, (int)CharStates.oneLegIdle);
            }
        } else {
            if (moveInput > 0 || moveInput < 0) {
                animator.SetInteger(animationState, (int)CharStates.twoLegWalk);
            } else if (jumpInput > 0) {
                animator.SetInteger(animationState, (int)CharStates.twoLegHop); 
            } else if (jumpInput < 0) {
                // needs work
                animator.SetInteger(animationState, (int)CharStates.twoLegCrouch); 
            } else {
                animator.SetInteger(animationState, (int)CharStates.twoLegIdle);
            }
        }
    }

    // MAINTAINS TIMERS
    void Timers()
    {
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
    }
    #endregion


    #region PHYSICS CHECKS
    void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
        {
            Turn();
        }
    }

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
        return (player.GetNumFeet() > 0) && (lastGroundedTime > 0);
    }

    bool CanJumpCut()
    {
        return isJumping && IsRising();
    }

    bool CanDoubleJump()
    {
        return (player.GetNumFeet() > 1) && (numJumps < 2);
    }
    #endregion CHECKS




    #region TRIGGERS
    void OnTriggerEnter(Collider collider)
    {
        string parentName = collider.transform.parent.name;
        Debug.Log(parentName);
        player.OnItemPickup(collider, parentName);
    }
    #endregion

}