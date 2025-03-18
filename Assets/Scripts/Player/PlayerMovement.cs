using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    private float speed = 4f;
    private float jumpingPower = 5f; 
    private bool isFacingRight = true;
    
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && playerRigidbody.velocity.y > 0f) {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * 0.5f);
        }
        playerAnim.SetFloat("Speed", Mathf.Abs(horizontal));
        Flip();
        playerAnim.SetBool("isJumping", !IsGrounded());
    }

    private void FixedUpdate() {
        playerRigidbody.velocity = new Vector2(horizontal * speed, playerRigidbody.velocity.y);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
