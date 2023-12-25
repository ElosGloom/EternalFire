using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private float destroyDelay;


        private Dictionary<string, int> _resources = new()
        {
            { "Wood", 0 }
        };

        private void AddItem(string itemName, int itemCount)
        {
            _resources[itemName] += itemCount;
        }

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
                AddItem(inventoryResource.ItemName, inventoryResource.ItemsCount);
                Destroy(inventoryResource.gameObject);
            }
        }
    }
}