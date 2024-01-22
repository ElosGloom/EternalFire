using Game.Scripts.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image hpFill;
        [SerializeField] private HealthComponent healthComponent;

        protected HealthComponent HealthComponent => healthComponent;

        private void Start()
        {
            healthComponent.HealthChangeEvent += OnHealthChanged;
        }

        protected virtual void OnHealthChanged()
        {
            hpFill.fillAmount =  healthComponent.CurrentHealth/ healthComponent.MaxHealth;
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}