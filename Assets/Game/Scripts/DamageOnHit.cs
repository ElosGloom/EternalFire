using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    // public class DamageOnHit : MonoBehaviour
    // {
    //     public int damageAmount;
    //     public DamageType damageType;
    //     [SerializeField] private Renderer _renderer;
    //
    //     private Dictionary<DamageType, Color> _dictionary = new()
    //     {
    //         { DamageType.Pure, Color.white },
    //         { DamageType.Physical, Color.red },
    //         { DamageType.Magical, Color.blue },
    //     };
    //
    //     private void Start()
    //     {
    //         _renderer.material.color = _dictionary[damageType];
    //     }
    //
    //     private void OnCollisionEnter(Collision other)
    //     {
    //         if (other.gameObject.CompareTag("Player"))
    //         {
    //             PlayerHealth healthComponent = other.gameObject.GetComponent<PlayerHealth>();
    //             if (healthComponent == null)
    //             {
    //                 return;
    //             }
    //
    //             healthComponent.TakeDamage(damageAmount, damageType);
    //         }
    //     }
    // }
}