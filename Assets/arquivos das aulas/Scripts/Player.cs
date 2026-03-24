using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 0.5f;

    private Rigidbody rb;
    private bool onGround;
    private Vector2 inputMovement;
    private Vector2 inputLook;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputValue value) => inputLook = value.Get<Vector2>();
    public void OnMove(InputValue value) => inputMovement = value.Get<Vector2>();

    public void OnJump()
    {
        if (onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    void Update()
    {  
        HandleRotation();
    }

    private void HandleRotation()
    {
        float mouseX = inputLook.x * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = (transform.right * inputMovement.x) + (transform.forward * inputMovement.y);
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) onGround = true;
    }
}
