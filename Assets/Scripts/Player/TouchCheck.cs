using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    
    CapsuleCollider2D playerColider;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

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

    private void Awake()
    {
        playerColider = GetComponent<CapsuleCollider2D>();   
    }

    void FixedUpdate()
    {
        IsGrounded = playerColider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
