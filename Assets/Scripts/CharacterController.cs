using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Basic values for the player speed.
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.31f;

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
    [SerializeField] float PlayerHeight = 2f;
    bool isGrounded;


    // Direction of the player.
    Vector3 moveDirection;

    // Rigidbody for player physics.
    Rigidbody rb;


    private void Start()
    {
        // Gets the players RB and freezes its rotation.
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Checking if the player is on the ground.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight / 2 + 0.01f);

        // Calling Player Input and Drag Control every frame.
        PlayerInput();
        ControlDrag();

        // Jumping input.
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        
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
        if (isGrounded)
        {
            // Multiplies the keyboard input by the movement speed to get the player movement.
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            // Multiplies the keyboard input by the movement speed to get the player movement.
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
