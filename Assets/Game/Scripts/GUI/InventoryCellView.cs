using TMPro;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class InventoryCellView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemCounterText;

        public void SetCounterText(int itemsCount)
        {
            itemCounterText.text = itemsCount.ToString();
        }
    }
}