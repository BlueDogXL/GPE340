using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Values")]
    public float currentHealth;
    public float maxHealth;
    private bool isDead;
    [SerializeField]
    private float initialHealth;
    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;

    public void Start()
    {
        // start at full health
        currentHealth = initialHealth;
        // be alive
        isDead = false;
    }
    public void TakeDamage(float damage)
    {
        // if we're not already dead
        if (!isDead)
        {
            // take damage
            currentHealth -= damage;
            // make sure we're not at a wacky amount
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            // invoke our event for anything else that should happen
            OnTakeDamage.Invoke();
            // cry about it
            Debug.Log(gameObject.name + " took " + damage + " damage!");
            // if we're dead
            if (currentHealth <= 0)
            {
                // die
                Die();
            }
        }
    }
    public void Heal(float healing)
    {
        // heal
        currentHealth += healing;
        // make sure we're not at a wacky amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        // invoke our event for anything else that should happen
        OnHeal.Invoke();
    }

    public void ResetHealth()
    {
        // make our health the initial value
        currentHealth = initialHealth;
    }
    public void Die()
    {
        // stay dead
        currentHealth = 0;
        // if we haven't died already
        if (isDead != true)
        {
            // we're dead now
            isDead = true;
            // call our event for dying
            OnDeath.Invoke();
        }
        
    }

    public float HealthPercent()
    {
        // get the percent
        float healthPercent = currentHealth / maxHealth;
        // send it back
        return healthPercent;
    }
}
