using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, InventoryCellView> cellViews;
        [SerializeField] private Inventory.Inventory inventory;


        private void Start()
        {
            foreach (var kvp in cellViews)
            {
                int itemCount = inventory.GetItemsCount(kvp.Key);
                UpdateCell(kvp.Key,itemCount);
            }

            inventory.ItemCountChangeEvent += UpdateCell;
        }

        private void UpdateCell(string itemName, int itemCount)
        {
            if (cellViews.ContainsKey(itemName))
            {
                cellViews[itemName].SetCounterText(itemCount);
            }
        }

        private void OnDestroy()
        {
            inventory.ItemCountChangeEvent -= UpdateCell;
        }
    }
}