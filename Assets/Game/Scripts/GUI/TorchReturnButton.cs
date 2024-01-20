using Game.Scripts.Fire;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class TorchReturnButton : MonoBehaviour
    {
        [SerializeField] private Image image;

        private void Awake()
        {
            FireSystem.HasTorchesToReturn += SetButtonState;
        }
        

        private void SetButtonState(bool hasTorches)
        {
            image.enabled = hasTorches;
        }

        private void OnDestroy()
        {
            FireSystem.HasTorchesToReturn -= SetButtonState;
        }
    }
}