using DefaultNamespace;
using Game.Scripts.Health;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField] private Rigidbody woodPrefab;
        [SerializeField] private Transform woodSpawnPoint;
        [SerializeField] private int shootOutForce;


        private void Start()
        {
            treeHP.HealthChangeEvent += OnDamageTaken;
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
                    float range = .5f;
                    float randomX = Random.Range(-range, range);
                    float randomY = Random.Range(-range, range);
                    float randomZ = Random.Range(-range, range);
                    var randomVector = new Vector3(randomX, randomY, randomZ);
                    var randomPosition = woodSpawnPoint.position + randomVector;


                    var currentPrefab = Instantiate(woodPrefab);
                    currentPrefab.transform.position = randomPosition;
                    currentPrefab.AddForce((randomPosition - transform.position) * shootOutForce);
                }

                Destroy(this);
            }

            animator.SetTrigger("TreeDamage");
        }

        private void OnDestroy()
        {
            treeHP.HealthChangeEvent -= OnDamageTaken;
        }
    }
}