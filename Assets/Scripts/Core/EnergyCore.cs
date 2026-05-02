using UnityEngine;
using System;

// Observer pattern: EnergyCore fires static C# events.
// Any system (HUD, GameManager, AudioManager) can subscribe without
// EnergyCore knowing who listens — loose coupling by design.
public class EnergyCore : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    public float MaxHealth    => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool  IsAlive      => currentHealth > 0f;

    // --- Observer events ---
    public static event Action<float, float> OnHealthChanged;  // (current, max)
    public static event Action               OnCoreDestroyed;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        currentHealth = Mathf.Max(0f, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
            OnCoreDestroyed?.Invoke();
    }
}
