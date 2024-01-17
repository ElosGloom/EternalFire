using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class HealthBarVisibility : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;

        private void Start()
        {
            healthComponent.HealthChangeEvent += OnHealthChanged;
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            gameObject.SetActive(currentHealth < maxHealth);
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}