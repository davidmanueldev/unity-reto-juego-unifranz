using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Run, Jump }
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSmoothTime = 0.1f;
    
    [Header("Jump & Gravity")]
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;
    public float gravityMultiplier = 2.0f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("State")]
    public PlayerState currentState = PlayerState.Idle;

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Main Camera not found. Please ensure the scene has a camera tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        CheckGrounded();
        HandleGravity();
        
        // State Machine Update
        switch (currentState)
        {
            case PlayerState.Idle:
                HandleMovement();
                HandleJump();
                break;
            case PlayerState.Run:
                HandleMovement();
                HandleJump();
                break;
            case PlayerState.Jump:
                HandleMovement(); // Allow air control
                HandleJump();
                break;
        }
    }

    void CheckGrounded()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        else
        {
            // Fallback ground check if transform is not assigned
            Vector3 bottom = transform.position - new Vector3(0, controller.height / 2, 0);
            isGrounded = Physics.CheckSphere(bottom, groundDistance, groundMask);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to stick to ground
        }
    }

    void HandleGravity()
    {
        velocity.y += gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculate target angle based on camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            
            // Smoothly rotate the player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player in the direction they are facing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            
            if (isGrounded)
            {
                ChangeState(PlayerState.Run);
            }
        }
        else if (isGrounded)
        {
            ChangeState(PlayerState.Idle);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMultiplier);
            ChangeState(PlayerState.Jump);
        }
    }

    void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        // Animation triggers will go here later
    }
}