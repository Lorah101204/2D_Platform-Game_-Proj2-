using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput;

    private bool isMoving = false;
    public bool IsMoving 
    { 
        get {return isMoving;} 
        private set 
        {
            isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private bool isFacingRight = true;
    public bool IsFacingRight 
    { 
        get {return isFacingRight;}
        private set 
        {
            if (isFacingRight != value)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            isFacingRight = value;
        }
    }

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFaciingDirection(moveInput);
    }

    private void SetFaciingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}