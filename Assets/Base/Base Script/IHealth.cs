using System;

public interface IHealth
{
    int Health { get; set; }
    int MaxHealth { get; set; }
    event Action<int, int> OnHealthChanged;

    void Heal(int amount);
    void ReduceHealth(int amount);
}