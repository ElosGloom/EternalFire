using System.Collections;
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

        public void AddItem(string itemName, int itemCount)
        {
            _resources[itemName] += itemCount;
        }

        public bool TryRemoveItem(string itemName, int itemCount)
        {
            if (itemCount < 0)
            {
                return false;
            }

            if (_resources[itemName] >= itemCount)
            {
                _resources[itemName] -= itemCount;
                return true;
            }

            return false;
        }
    }
}