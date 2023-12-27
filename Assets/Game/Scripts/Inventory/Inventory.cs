using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public Action<string, int> ItemCountChangeEvent;

        private Dictionary<string, int> _items = new();


        public void AddItem(string itemName, int itemCount)
        {
            if (!_items.ContainsKey(itemName))
                _items.Add(itemName, itemCount);
            else
                _items[itemName] += itemCount;

            ItemCountChangeEvent?.Invoke(itemName, _items[itemName]);
        }

        public bool TryRemoveItem(string itemName, int itemCount)
        {
            if (itemCount < 0)
                return false;

            if (!_items.ContainsKey(itemName))
                return false;

            if (_items[itemName] < itemCount)
                return false;

            _items[itemName] -= itemCount;
            ItemCountChangeEvent?.Invoke(itemName, _items[itemName]);
            return true;
        }
    }
}