using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;

    TouchCheck touchCheck;
    Damageable damageable;

    public float CurrentSpeed 
    {
        get 
        {
            if (CanMove)
            {
                if (IsMoving && !touchCheck.IsOnWall)
                {
                    if (touchCheck.IsGrounded)
                    {
                        return walkSpeed;
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

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

    public bool CanMove 
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool IsAlive 
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchCheck = GetComponent<TouchCheck>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity) rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive) 
        {
            IsMoving = moveInput != Vector2.zero;

            SetFaciingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchCheck.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
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