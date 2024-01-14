using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class InventoryCollector : MonoBehaviour
    {
        
        [SerializeField] private float destroyDelay;
        [SerializeField] private Inventory.Inventory _inventory;

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(DestroyDelayRoutine(other));
        }

        private IEnumerator DestroyDelayRoutine(Collider other)
        {
            var inventoryResource = other.gameObject.GetComponent<InventoryResource>();
            yield return new WaitForSeconds(destroyDelay);

            if (inventoryResource)
            {
                _inventory.AddItem(inventoryResource.ItemName, inventoryResource.ItemsCount);
                Destroy(inventoryResource.gameObject);
            }
        }
    }
}