using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Game.Scripts
{
    public class TreeCutter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float attackCooldown = 1f;
        private float _nextAttackTimeLeft;
        [SerializeField] private int damage;
        [SerializeField] private float damageDelay;


        private void Start()
        {
            _nextAttackTimeLeft = 0;
            
        }

        private void Update()
        {
            _nextAttackTimeLeft -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_nextAttackTimeLeft < 0)
            {
                if (other.gameObject.CompareTag("Tree"))
                {
                    animator.SetTrigger("TreeCut");
                    StartCoroutine(CooldownResetRoutine());

                    StartCoroutine(DamageDelayRoutine(other));
                    
                }
            }
        }

        private IEnumerator CooldownResetRoutine()
        {
            yield return new WaitForFixedUpdate();
            _nextAttackTimeLeft = attackCooldown;

        }

        private IEnumerator DamageDelayRoutine(Collider other)
        {
            yield return new WaitForSeconds(damageDelay);
            var comp = other.GetComponent<HealthComponent>();
            comp.TakeDamage(damage);
            
        }
    }
}