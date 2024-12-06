using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Values")]
    public float currentHealth;
    public float maxHealth;
    [SerializeField]
    private float initialHealth;
    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;

    public void Start()
    {
        currentHealth = initialHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // take damage
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // make sure we're not at a wacky amount
        OnTakeDamage.Invoke(); // event for anything else that should happen
        Debug.Log(gameObject.name + " took " + damage + " damage!");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHeal.Invoke();
    }

    public void ResetHealth()
    {
        currentHealth = initialHealth;
    }
    public void Die()
    {
        currentHealth = 0;
        OnDeath.Invoke();
    }

    public float HealthPercent()
    {
        float healthPercent = currentHealth / maxHealth;
        return healthPercent;
    }
}
