using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    
    [SerializeField] CapsuleCollider2D playerColider;
    [SerializeField] Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private bool isGrounded;
    public bool IsGrounded 
    { 
        get
        {
            return isGrounded; 
        }
        private set
        {
            isGrounded = value; 
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private bool isOnWall;
    public bool IsOnWall
    { 
        get
        {
            return isOnWall; 
        }
        private set
        {
            isOnWall = value; 
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    private bool isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsOnCeiling
    { 
        get
        {
            return isOnCeiling; 
        }
        private set
        {
            isOnCeiling = value; 
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        playerColider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();   
    }

    void FixedUpdate()
    {
        IsGrounded = playerColider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = playerColider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = playerColider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
