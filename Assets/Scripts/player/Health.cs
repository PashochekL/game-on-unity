using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public int MaxHealth => maxHealth;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;
   

    private void Awake()
    {

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {

        if (currentHealth <= 0) return;

        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {

            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Min(currentHealth+ amount, maxHealth);
        OnHealthChanged?. Invoke(currentHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
