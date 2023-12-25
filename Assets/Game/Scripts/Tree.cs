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
        [SerializeField] private MeshFilter treeMesh;
        [SerializeField] private Mesh damagedMesh;
        [SerializeField] private Mesh destroyedMesh;
        [SerializeField] private Collider treeTrigger;
        [SerializeField] private Collider treeCollider;
        public int woodCount;
        [SerializeField] private GameObject woodPrefab;


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

            if (currentHp <= 0.5 * maxHp)
            {
                treeMesh.mesh = damagedMesh;
            }

            if (currentHp <= 0)
            {
                treeMesh.mesh = destroyedMesh;
                Destroy(treeTrigger);
                Destroy(treeCollider);
                Destroy(treeHP);
                Destroy(animator, 1);
                for (int i = 0; i < woodCount; i++)
                {
                    Instantiate(woodPrefab).transform.position = transform.position;
                }

                Destroy(this);
            }

            animator.SetTrigger("TreeDamage");
        }

        private void OnDestroy()
        {
            treeHP.OnHealthChangeEvent -= OnDamageTaken;
        }
    }
}