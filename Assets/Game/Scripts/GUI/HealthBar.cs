using Game.Scripts.Health;
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
            healthComponent.HealthChangeEvent += OnHealthChanged;
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            hpText.text = $"{currentHealth.ToString("0")}/{maxHealth.ToString()}";
            hpFill.fillAmount = currentHealth / maxHealth;
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}