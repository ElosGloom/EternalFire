using FPS.Pool;
using Game.Scripts.Health;
using Game.Scripts.Inventory;
using UnityEngine;

namespace Game.Scripts
{
    public class ForestTree : MonoBehaviour
    {

        private static readonly int TreeDamage = Animator.StringToHash("TreeDamage");
        [SerializeField] private HealthComponent treeHp;
        [SerializeField] private Animator animator;
        [SerializeField] private MeshFilter treeMesh;
        [SerializeField] private Mesh damagedMesh;
        [SerializeField] private Mesh destroyedMesh;
        [SerializeField] private Collider treeTrigger;
        [SerializeField] private Collider treeCollider;
        [SerializeField] private Transform woodSpawnPoint;
        [SerializeField] private int shootOutForce;
        [SerializeField] private int woodCount;


        private void Start()
        {
            treeHp.HealthChangeEvent += OnDamageTaken;
        }

        private void OnDestroy()
        {
            treeHp.HealthChangeEvent -= OnDamageTaken;
        }

        private void OnDamageTaken()
        {
            float currentHp = treeHp.CurrentHealth;
            float maxHp = treeHp.MaxHealth;
            if (currentHp == maxHp) //todo why?
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
                for (var i = 0; i < woodCount; i++)
                {
                    var range = .5f;
                    float randomX = Random.Range(-range, range);
                    float randomY = Random.Range(-range, range);
                    float randomZ = Random.Range(-range, range);
                    var randomVector = new Vector3(randomX, randomY, randomZ);
                    Vector3 randomPosition = woodSpawnPoint.position + randomVector;


                    var inventoryResource = FluffyPool.Get<InventoryResource>("wood");
                    inventoryResource.transform.position = randomPosition;
                    inventoryResource.transform.SetParent(transform.parent);
                    inventoryResource.Rigidbody.AddForce((randomPosition - transform.position) * shootOutForce);
                }

                Destroy(this);
            }

            animator.SetTrigger(TreeDamage);
        }
    }
}