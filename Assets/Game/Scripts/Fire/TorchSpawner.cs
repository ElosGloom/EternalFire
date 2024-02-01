using Game.Scripts.Health;
using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class TorchSpawner : MonoBehaviour
    {
        [SerializeField] private Torch torchPrefab;
        [SerializeField] private HealthComponent fireSystemHealth;
        
        [SerializeField] private Transform fireParent;
        [SerializeField] private Inventory.Inventory inventory;


        private const float MinSpawnRadius = 5f;
        private const float MaxSpawnRadius = 5.5f;


        private void Update()
        {
            TrySpawnTorch();
        }

        private void TrySpawnTorch()
        {
            var spawnerPosition = transform.position;
            var nearestMember = FireSystem.Instance.SearchNearestMember(spawnerPosition);

            var distance = Vector3.Distance(spawnerPosition, nearestMember.transform.position);
            
            if (FireSystem.Instance.HealthComponent.CurrentHealth<=0)
                return;
            
            if (distance < MinSpawnRadius || distance > MaxSpawnRadius)
                return;

            if (!inventory.TryRemoveItem("Torch", 1))
                return;

            var torch = Instantiate(torchPrefab, spawnerPosition, Quaternion.identity, fireParent);
            FireSystem.Instance.ConnectNewTorch(torch, nearestMember);
            FireSystem.Instance.TryConnectBonfire(MaxSpawnRadius, torch);
            SfxController.PlaySfx("event:/Torch");
        }
    }
}