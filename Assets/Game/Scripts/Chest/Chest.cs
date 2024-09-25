using System;
using FPS.Pool;
using Game.Scripts.Health;
using Game.Scripts.Inventory;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Chest
{
    public class Chest : MonoBehaviour
    {
        [field: SerializeField] public SerializableDictionary<ChestContent, GameObject> Content { get; private set; }
       
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.GetComponent<TreeCutter>())
            {
                GiveRandomReward();
            }
        }
        
        private void GiveRandomReward()
        {
            var randomValue = Content.GetRandomKey();
            Content.TryGetValue(randomValue, out var value);
            if (value != null)
            {
                SpawnReward(randomValue);
                Destroy(this.gameObject);
            }
        }

        private void SpawnReward(ChestContent value)
        {
            var inventoryResource = FluffyPool.Get<InventoryResource>(value.ToString());
            inventoryResource.transform.position = transform.position;
            inventoryResource.transform.SetParent(transform.parent);
        }
    }
}