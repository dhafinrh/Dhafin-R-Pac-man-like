using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Transform respawnPoint;

    private void Start()
    {
        Health = maxHealth;
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }

    public void TakeDamage(int amount)
    {
        ReduceHealth(amount);
        if (Health <= 0)
            Die();
        else
            transform.position = respawnPoint.position;
    }

    public void Die()
    {
        Debug.Log("Player Kalah");
        OnPlayerDeath?.Invoke();
    }

    public int Health { get; set; }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public event Action<int, int> OnHealthChanged;

    public void Heal(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }

    public void ReduceHealth(int amount)
    {
        Health = Mathf.Max(Health - amount, 0);
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }

    public static event Action OnPlayerDeath;
}