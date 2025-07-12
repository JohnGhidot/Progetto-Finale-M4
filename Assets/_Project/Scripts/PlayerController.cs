using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _Speed = 5f;
    [SerializeField] private float _JumpForce = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [SerializeField] private Transform cameraPivot;

    private Rigidbody _rb;
    private bool isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGroundStatus();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (cameraPivot == null) return;

        Vector3 forward = cameraPivot.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraPivot.right;
        right.y = 0f;
        right.Normalize();

        Vector3 direction = (right * h + forward * v);

        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 velocity = direction * _Speed;
        velocity.y = _rb.velocity.y;

        _rb.velocity = velocity;
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * _JumpForce, ForceMode.Impulse);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
