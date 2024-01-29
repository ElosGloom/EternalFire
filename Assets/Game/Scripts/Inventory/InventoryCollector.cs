using System;
using System.Collections;
using FPS.Pool;
using Game.Scripts.Inventory;
using UnityEngine;

namespace Game.Scripts
{
    public class InventoryCollector : MonoBehaviour
    {
        [SerializeField] private Inventory.Inventory inventory;


        private void OnTriggerEnter(Collider other)
        {
            var inventoryResource = other.gameObject.GetComponent<InventoryResource>();
            if (inventoryResource)
            {
                try
                {
                    inventoryResource.curveMotion.Move(other.transform.position, transform, () =>
                    {
                        inventory.AddItem(inventoryResource.ItemName, inventoryResource.ItemsCount);
                        FluffyPool.Return(inventoryResource);
                    });
                }
                catch (Exception e)
                {
                    throw;
                }
              
            }
        }
    }
}