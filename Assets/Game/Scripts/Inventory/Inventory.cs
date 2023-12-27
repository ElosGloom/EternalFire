using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public Action<string,int> ItemCountChangeEvent;

        private Dictionary<string, int> _resources = new()
        {
            { "Wood", 0 },
            { "Coin", 0 }
        };


        public void AddItem(string itemName, int itemCount)
        {
            _resources[itemName] += itemCount;
            ItemCountChangeEvent?.Invoke(itemName,_resources[itemName]);
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
                ItemCountChangeEvent?.Invoke(itemName,_resources[itemName] );
                return true;
            }

            return false;
        }
    }
}