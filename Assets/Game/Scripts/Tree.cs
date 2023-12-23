using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Game.Scripts
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private HealthComponent treeHP;
        [SerializeField] private Animator animator;


        private void Start()
        {
            treeHP.OnHealthChangeEvent += OnDamageTaken;
        }


        private void OnDamageTaken(float currentHp, float maxHp)
        {
            if (currentHp == maxHp)
            {
                return;
            }

            if (currentHp <= 0)
            {
                Destroy(treeHP.gameObject);
            }

            animator.SetTrigger("TreeDamage");
        }

        private void OnDestroy()
        {
            treeHP.OnHealthChangeEvent -= OnDamageTaken;
        }
    }
}