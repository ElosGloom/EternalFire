using System;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action<float, float> HealthChangeEvent;
        public event Action OnDeathEvent;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;


        private void OnValidate()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;


            if (currentHealth < 1)
            {
                OnDeathEvent?.Invoke();
            }

            HealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }

        public void Healing(int heal)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            HealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }
    }
}