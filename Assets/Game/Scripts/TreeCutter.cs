using System;
using UnityEngine;

namespace Game.Scripts
{
    public class TreeCutter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float attackCooldown = 1f;
        private float _nextAttackTimeLeft;

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
                    _nextAttackTimeLeft = attackCooldown;
                }
            }
        }
    }
}