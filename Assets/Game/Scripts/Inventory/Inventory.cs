using System;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public Action<string, int> ItemCountChangeEvent;

        [SerializeField] private SerializableDictionary<string, int> items = new();


        public void AddItem(string itemName, int itemCount)
        {
            if (!items.ContainsKey(itemName))
                items.Add(itemName, itemCount);
            else
                items[itemName] += itemCount;

            ItemCountChangeEvent?.Invoke(itemName, items[itemName]);
        }

        public bool TryRemoveItem(string itemName, int itemCount)
        {
            if (itemCount < 0)
                return false;

            if (!items.ContainsKey(itemName))
                return false;

            if (items[itemName] < itemCount)
                return false;

            items[itemName] -= itemCount;
            ItemCountChangeEvent?.Invoke(itemName, items[itemName]);
            return true;
        }
    }
}