using System;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

namespace DefaultNamespace
{
     public class HealthComponent : MonoBehaviour
     {
         public event Action<float, float> OnHealthChangeEvent;
         public  event Action OnDeathEvent;
         [SerializeField] private float maxHealth ;
         [SerializeField] private float currentHealth;

       

         private void Start()
         {
             currentHealth = maxHealth;
             OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
         }

         public void TakeDamage(int damage)
         {
             currentHealth -= damage;


             if (currentHealth < 1)
             {
                 OnDeathEvent?.Invoke();
             }

             OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
         }

         public void Healing(int heal)
         {
             currentHealth += heal;
             if (currentHealth > maxHealth)
             {
                 currentHealth = maxHealth;
             }

             OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
             
         }
     }
}