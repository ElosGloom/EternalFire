using UnityEngine;

namespace Game.Scripts.Fire
{
    public class TorchDestroyer : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DestroyLastTorch();
            }
        }

        private void DestroyLastTorch()
        {
            if (FireSystem.Instance.TryUnregisterLastTorch(out var lastTorch))
            {
                Destroy(lastTorch.gameObject);
            }
        }
    }
}