using System;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action<float, float> HealthChangeEvent;
        public event Action DeathEvent;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public float MaxHealth => maxHealth;

        public float CurrentHealth => currentHealth;

        private void OnValidate()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (currentHealth <= 0)
                return;

            currentHealth -= damage;

            if (currentHealth <= 0)
                DeathEvent?.Invoke();

            HealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }

        public void Healing(int heal)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            HealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }
    }
}