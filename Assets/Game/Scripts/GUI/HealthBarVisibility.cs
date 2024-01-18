using Game.Scripts.Health;
using Game.Scripts.States;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class HealthBarVisibility : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField, Min(0.1f)] private float fadeTime = 1;

        private readonly StateMachine _stateMachine = new();

        private void Start()
        {
            var visibleState = new HealthBarVisibleState(canvasGroup, fadeTime, healthComponent, _stateMachine);
            _stateMachine.AddState(visibleState);

            var invisibleState = new HealthBarInvisibleState(canvasGroup, fadeTime, healthComponent, _stateMachine);
            _stateMachine.AddState(invisibleState);

            _stateMachine.SetState<HealthBarInvisibleState>();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}