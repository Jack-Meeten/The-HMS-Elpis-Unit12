using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Basic values for the player speed.
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.31f;
    public float movementMultiplier = 10f;

    // Jumping values.
    [Header("Jumping")]
    [SerializeField] float JumpForce = 6f;

    // Key mapping.
    [Header("Keybindings")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    // Drag of the player with the ground.
    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 3f;

    // Movement input floats.
    float horizontalMovement;
    float verticalMovement;

    [SerializeField] Transform orientation;

    // Jumping.
    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float PlayerHeight = 2f;
    bool isGrounded;
    float groundDistance = 0.4f;


    // Direction of the player.
    Vector3 moveDirection;

    // Rigidbody for player physics.
    Rigidbody rb;

    // Handling Slopes.
    RaycastHit slopeHit;
    Vector3 slopeMoveDir;

    private bool OnSlope()
    {
        Debug.DrawRay(transform.position, Vector3.down * (PlayerHeight / 2 + 0.5f));
        // Player raycasting from the point of their feet.
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, PlayerHeight / 2 + 0.4f))
        {
            // Using raycast to check if the player is not on normal ground == slope.
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    private void Start()
    {
        // Gets the players RB and freezes its rotation.
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Checking if the player is on the ground.
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        // Calling Player Input and Drag Control every frame.
        PlayerInput();
        ControlDrag();

        // Jumping input.
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        // Slope moving.
        slopeMoveDir = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void PlayerInput()
    {
        // Gets the horizontal and the vertical input from the keyboard and sets the horizontal and vertical floats to the same value.
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        // Sets the moving direction to the multiplication of the vertical and horizontal movement.
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        // Jumping mechanics, gets the rigid body's location and adds an impulse equal to the JumpForce.
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    void ControlDrag()
    {
        // Sets the RB drag to ground drag if the player is on the ground but when he is not on the ground RB drag gets set to airDrag.
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            // Multiplies the keyboard input by the movement speed to get the player movement.
            //Debug.Log("Grounded but not in slope");
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            Debug.Log("Hitting Slope");
            rb.AddForce(slopeMoveDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            // Multiplies the keyboard input by the movement speed to get the player movement.
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
