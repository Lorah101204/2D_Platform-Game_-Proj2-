using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPick : MonoBehaviour
{
    public int healthAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            damageable.Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}
