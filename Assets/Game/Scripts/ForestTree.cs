using Game.Scripts.Health;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class ForestTree : MonoBehaviour
    {
        [SerializeField] private HealthComponent treeHp;
        [SerializeField] private Animator animator;
        [SerializeField] private MeshFilter treeMesh;
        [SerializeField] private Mesh damagedMesh;
        [SerializeField] private Mesh destroyedMesh;
        [SerializeField] private Collider treeTrigger;
        [SerializeField] private Collider treeCollider;
        [SerializeField] private Rigidbody woodPrefab;
        [SerializeField] private Transform woodSpawnPoint;
        [SerializeField] private int shootOutForce;
        [SerializeField] private int woodCount;
        
        private static readonly int TreeDamage = Animator.StringToHash("TreeDamage");

        
        private void Start()
        {
            treeHp.HealthChangeEvent += OnDamageTaken;
        }

        private void OnDamageTaken(float currentHp, float maxHp)
        {
            if (currentHp == maxHp)//todo why?
                return;

            if (currentHp <= 0.5 * maxHp) 
                treeMesh.mesh = damagedMesh;

            if (currentHp <= 0)
            {
                treeMesh.mesh = destroyedMesh;
                Destroy(treeTrigger);
                Destroy(treeCollider);
                Destroy(treeHp);
                Destroy(animator, 1);
                for (int i = 0; i < woodCount; i++)
                {
                    float range = .5f;
                    float randomX = Random.Range(-range, range);
                    float randomY = Random.Range(-range, range);
                    float randomZ = Random.Range(-range, range);
                    var randomVector = new Vector3(randomX, randomY, randomZ);
                    var randomPosition = woodSpawnPoint.position + randomVector;


                    var currentPrefab = Instantiate(woodPrefab,randomPosition, Quaternion.identity, transform.parent);
                    currentPrefab.AddForce((randomPosition - transform.position) * shootOutForce);
                }

                Destroy(this);
            }

            animator.SetTrigger(TreeDamage);
        }

        private void OnDestroy()
        {
            treeHp.HealthChangeEvent -= OnDamageTaken;
        }
    }
}