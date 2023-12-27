using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image hpFill;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private HealthComponent healthComponent;

        private void Start()
        {
            healthComponent.OnHealthChangeEvent += OnHealthChangeEvent;
        }

        private void OnHealthChangeEvent(float currentHealth, float maxHealth)
        {
            hpText.text = $"{currentHealth.ToString("0")}/{maxHealth.ToString()}";
            hpFill.fillAmount = currentHealth / maxHealth;
        }

        private void OnDestroy()
        {
            healthComponent.OnHealthChangeEvent -= OnHealthChangeEvent;
        }
    }
}