using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class InventoryResource : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private int itemsCount = 1;

        public string ItemName => itemName;

        public int ItemsCount => itemsCount;
    }
}