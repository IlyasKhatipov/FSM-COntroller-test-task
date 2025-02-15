using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform cameraTransform;
    private Animator animator;
    private Rigidbody rb;

    private enum PlayerState
    {
        Idle,
        Running,
        Jumping
    }

    private PlayerState currentState;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentState = PlayerState.Idle;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleState();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        if (movement.magnitude > 0)
        {
            if (currentState != PlayerState.Running)
            {
                currentState = PlayerState.Running;
            }
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            if (currentState != PlayerState.Idle)
            {
                currentState = PlayerState.Idle;
            }
        }

        if (Input.GetButtonDown("Jump") && currentState != PlayerState.Jumping)
        {
            currentState = PlayerState.Jumping;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            cameraTransform.Rotate(Vector3.up, horizontalInput);
        }
    }

    void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                animator.Play("Idle");
                break;
            case PlayerState.Running:
                animator.Play("Run");
                break;
            case PlayerState.Jumping:
                animator.Play("Jump");
                break;
        }
    }
}
