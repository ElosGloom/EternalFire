using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class HealthBarVisibility : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool currentState;
        [SerializeField, Min(0.1f)] private float fadeTime = 1;


        private void Start()
        {
            healthComponent.HealthChangeEvent += OnHealthChanged;
            if (currentState)
                canvasGroup.alpha = 1;
            else
                canvasGroup.alpha = 0;
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            currentState = currentHealth < maxHealth;
        }

        private void Update()
        {
            if (currentState)
                canvasGroup.alpha += Time.deltaTime / fadeTime;
            else
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}