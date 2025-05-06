using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed = 3f;
    public float walkStopRate = 0.6f;
    private bool hasFlipped = false;
    
    public DetectionZone atkZone;

    [SerializeField] private Rigidbody2D rb;
    TouchCheck touchCheck;
    [SerializeField] private Animator animator;
    Damageable damageable;

    public enum WalkAbleDirection 
    {
        Left,
        Right
    }

    private WalkAbleDirection walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkAbleDirection WalkDirection
    {
        get { return walkDirection; }
        set
        {
            if (walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkAbleDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkAbleDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            walkDirection = value;
        }
    }

    public bool hasTarget = false;
    public bool HasTarget 
    { 
        get { return hasTarget; }
        set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove 
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        touchCheck = GetComponent<TouchCheck>(); 
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = atkZone.colliders.Count > 0;
    } 

    private void FixedUpdate()
    {
        if (!hasFlipped && touchCheck.IsGrounded && touchCheck.IsOnWall)
        {
            Flip();
            hasFlipped = true;
        }
        else if (touchCheck.IsOnWall || !touchCheck.IsGrounded)
        {
            hasFlipped = false;
        }
        rb.velocity = new Vector2(speed * walkDirectionVector.x, rb.velocity.y);

        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(speed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    private void Flip()
    {
        if (WalkDirection == WalkAbleDirection.Right)
        {
            WalkDirection = WalkAbleDirection.Left;
        }
        else if (WalkDirection == WalkAbleDirection.Left)
        {
            WalkDirection = WalkAbleDirection.Right;
        }
        else
        {
            Debug.LogError("WalkDirection is not set to Left or Right.");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
