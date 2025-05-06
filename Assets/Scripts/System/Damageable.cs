using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    [SerializeField] private Animator animator;
    [SerializeField] private int maxHealth = 100;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
        }
    }

    [SerializeField] private int health = 100;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private bool isAlive = true;
    private bool isInvincible = false;
    private float timeSinceHit = 0f;
    public float invincibilityTime = 0.25f;
    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    public bool LockVelocity 
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set 
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0f;
            }
            timeSinceHit += Time.deltaTime;    
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            Debug.Log("Hit! Health: " + Health);

            LockVelocity = true;
            animator.SetTrigger(AnimationStrings.hit);
            damageableHit?.Invoke(damage, knockback);

            return true;
        }

        return false;
    }
}
