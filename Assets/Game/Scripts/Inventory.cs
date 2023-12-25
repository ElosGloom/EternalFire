using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class Inventory : MonoBehaviour
    {
        private Dictionary<string, int> _resources = new()
        {
            { "Wood", 0 }
        };

        private void AddItem(string itemName, int itemCount)
        {
           
            _resources[itemName] += itemCount;
            Debug.Log(_resources["Wood"]);
        }

        private void OnTriggerEnter(Collider other)
        {
            InventoryResource inventoryresource = other.gameObject.GetComponent<InventoryResource>();
            if (other.CompareTag("Resource"))
            {
                AddItem(inventoryresource.ItemName,inventoryresource.ItemsCount);
                Destroy(inventoryresource.gameObject);
            }
          
        }
        
    }
}