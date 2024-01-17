using Game.Scripts.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image hpFill;
        [SerializeField] private HealthComponent healthComponent;

        private void Start()
        {
            healthComponent.HealthChangeEvent += OnHealthChanged;
        }

        protected virtual void OnHealthChanged(float currentHealth, float maxHealth)
        {
            hpFill.fillAmount = currentHealth / maxHealth;
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}