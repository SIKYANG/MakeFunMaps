using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float mouseSpeed = 8f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float gravity = 10f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float mouseX;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleJump();
    }

    void HandleMouseLook()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);
    }

    void HandleMovement()
    {
        if (controller.isGrounded)
        {
            // Capture input and move in the direction
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, 0, moveZ);
            move = transform.TransformDirection(move);
            move = move * speed;
            move.y = moveDirection.y; // Preserve the y velocity

            moveDirection = move;
        }
        else
        {
            // Apply gravity if in the air
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleJump()
    {
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpForce;
         }
    }
}
