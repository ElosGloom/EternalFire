using Game.Scripts.Fire;
using TMPro;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class InactiveBonfiresCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private void Start()
        {
           UpdateCounter();
            FireSystem.InactiveBonfiresCountChangeEvent += UpdateCounter;
        }

        private void OnDestroy()
        {
            FireSystem.InactiveBonfiresCountChangeEvent += UpdateCounter;
        }

        private void UpdateCounter()
        {
            var bonfiresCount= FireSystem.Instance.GetInactiveBonfiresCount();
            counterText.text = bonfiresCount.ToString();
        }
    }
}
