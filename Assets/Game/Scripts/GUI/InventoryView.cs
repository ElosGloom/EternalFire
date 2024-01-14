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
            inventory.ItemCountChangeEvent += OnItemCountChange;
        }

        public void OnItemCountChange(string itemName, int itemCount)
        {
            if (cellViews.ContainsKey(itemName))
            {
                cellViews[itemName].ItemCounterText.text = itemCount.ToString();
            }
           
        }

        private void OnDestroy()
        {
            inventory.ItemCountChangeEvent -= OnItemCountChange;
        }
    }
}