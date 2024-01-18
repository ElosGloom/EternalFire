using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.States
{
    public class HealthBarInvisibleState : IState
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly float _fadeTime;
        private readonly HealthComponent _healthComponent;
        private readonly StateMachine _stateMachine;

        public HealthBarInvisibleState(
            CanvasGroup canvasGroup,
            float fadeTime,
            HealthComponent healthComponent,
            StateMachine stateMachine)
        {
            _canvasGroup = canvasGroup;
            _fadeTime = fadeTime;
            _healthComponent = healthComponent;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _healthComponent.HealthChangeEvent += OnHealthChanged;
        }

        public void Exit()
        {
            _healthComponent.HealthChangeEvent -= OnHealthChanged;
        }

        public void Update()
        {
            _canvasGroup.alpha -= Time.deltaTime / _fadeTime;
        }

        private void OnHealthChanged(float currentHealth, float maxHealth)
        {
            if (currentHealth < maxHealth)
                _stateMachine.SetState<HealthBarVisibleState>();
        }
    }
}