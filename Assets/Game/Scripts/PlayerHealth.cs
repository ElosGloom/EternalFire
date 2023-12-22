using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
//     public class PlayerHealth : MonoBehaviour
//     {
//         public static event Action<float, float> OnHealthChangeEvent;
//         public static event Action OnDeathEvent;
//         [SerializeField] private float maxHealth = 100;
//         [SerializeField] private float currentHealth;
//
//         private Dictionary<DamageType, float> _damageMultipliers = new()
//         {
//             { DamageType.Pure, 1 },
//             { DamageType.Physical, 0.8f },
//             { DamageType.Magical, 0.5f }
//         };
//
//         private void Start()
//         {
//             currentHealth = maxHealth;
//             OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
//         }
//
//         public void TakeDamage(int damage, DamageType damageType)
//         {
//             currentHealth -= _damageMultipliers[damageType] * damage;
//
//
//             if (currentHealth < 1)
//             {
//                 OnDeathEvent?.Invoke();
//             }
//
//             OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
//         }
//
//         public void Healing(int heal)
//         {
//             currentHealth += heal;
//             if (currentHealth > maxHealth)
//             {
//                 currentHealth = maxHealth;
//             }
//
//             OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
//         }
//     }
}