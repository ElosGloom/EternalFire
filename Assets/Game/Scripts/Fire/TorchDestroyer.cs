using UnityEngine;

namespace Game.Scripts.Fire
{
    public class TorchDestroyer : MonoBehaviour
    {
        [SerializeField] private Inventory.Inventory inventory;

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DestroyLastTorch();
            }
        }

        private void DestroyLastTorch()
        {
            if (!FireSystem.Instance.TryDisconnectLastTorch(out var lastTorch))
                return;

            inventory.AddItem("Torch", 1);
            Destroy(lastTorch.gameObject);
        }
    }
}