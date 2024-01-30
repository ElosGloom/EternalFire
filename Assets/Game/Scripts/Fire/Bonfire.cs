using FPS.Pool;
using Game.Scripts.Inventory;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class Bonfire : FireSystemMember
    {
        [SerializeField] private float woodTimeRequest = 0.25f;
        [SerializeField] private GameObject revealerState;


        private float _timeToNextRequest;


        private void Update()
        {
            _timeToNextRequest -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (enabled == false)
                return;

            if (_timeToNextRequest > 0)
                return;

            if (!other.TryGetComponent(out Inventory.Inventory inventory))
                return;

            if (!inventory.TryRemoveItem("Wood", 1))
                return;

            var inventoryResource = FluffyPool.Get<InventoryResource>("wood");
            inventoryResource.ItemCollider.enabled = false;
            inventoryResource.transform.SetParent(transform.parent);

            inventoryResource.curveMotion.Move(other.transform.position, transform, () =>
            {
                FluffyPool.Return(inventoryResource);
                inventoryResource.ItemCollider.enabled = true;
            });

            _timeToNextRequest = woodTimeRequest;
            FireSystem.Instance.HealthComponent.Healing(3);
        }

        public void SwitchActive(bool value)
        {
            revealerState.SetActive(value);
            enabled = value;
        }
    }
}