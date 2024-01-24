using System.Collections;
using FPS.Pool;
using Game.Scripts.Inventory;
using UnityEngine;

namespace Game.Scripts
{
    public class InventoryCollector : MonoBehaviour
    {
        [SerializeField] private float destroyDelay;
        [SerializeField] private Inventory.Inventory inventory;

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(ReturnInPoolDelayRoutine(other));
            other.enabled = false;
        }

        private IEnumerator ReturnInPoolDelayRoutine(Collider other)
        {
            var inventoryResource = other.gameObject.GetComponent<InventoryResource>();
            yield return new WaitForSeconds(destroyDelay);

            if (inventoryResource)
            {
                inventory.AddItem(inventoryResource.ItemName, inventoryResource.ItemsCount);
                FluffyPool.Return(inventoryResource);
                other.enabled = true;
            }
        }
    }
}