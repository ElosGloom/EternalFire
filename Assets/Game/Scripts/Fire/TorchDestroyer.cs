using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Fire
{
    public class TorchDestroyer : MonoBehaviour
    {
        [SerializeField] private Inventory.Inventory inventory;
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(DestroyLastTorch);
        }

        private void DestroyLastTorch()
        {
            if (!FireSystem.Instance.TryDisconnectLastTorch(out var lastTorch))
            {
                return;
            }
            

            inventory.AddItem("Torch", 1);
            Destroy(lastTorch.gameObject);
        }
    }
}