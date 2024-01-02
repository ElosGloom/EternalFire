using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action<float, float> HealthChangeEvent;
        public event Action OnDeathEvent;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        

        private void Start()
        {
            currentHealth = maxHealth;
            HealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;


            if (currentHealth < 1)
            {
                OnDeathEvent?.Invoke();
            }

            HealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }

        // public void DecreaseHealthOverTime()
        // {
        //     float decreaseAmount = 1f; 
        //     float decreaseInterval = 1f; 
        //
        //     StartCoroutine(DecreaseHealthRoutine(decreaseAmount, decreaseInterval));
        // }
        //
        // IEnumerator DecreaseHealthRoutine(float decreaseAmount, float decreaseInterval)
        // {
        //     while (currentHealth > 0f)
        //     {
        //         yield return new WaitForSeconds(decreaseInterval);
        //         currentHealth -= decreaseAmount;
        //
        //       
        //         if (currentHealth <= 0f)
        //         {
        //             // вуфер
        //         }
        //     }
        // }

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