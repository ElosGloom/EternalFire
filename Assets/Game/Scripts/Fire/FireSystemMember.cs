using FoW;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireSystemMember : MonoBehaviour
    {
        [SerializeField] private FogRevealer fogRevealer;
        [SerializeField] private AnimationCurve lightRadiusCurve;
        [SerializeField] private AnimationCurve revealForceCurve;
        [SerializeField] private ParticleSystemForceField forceField;

        public ParticleSystemForceField ForceField => forceField;

        private void Awake()
        {
            var healthComponent = FireSystem.Instance.HealthComponent;
            OnHealthChanged();
            healthComponent.HealthChangeEvent += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            var currentHp = FireSystem.Instance.HealthComponent.CurrentHealth;
            var maxHp = FireSystem.Instance.HealthComponent.MaxHealth;
            fogRevealer.Radius = lightRadiusCurve.Evaluate(currentHp / maxHp);
            fogRevealer.RevealForce = revealForceCurve.Evaluate(currentHp / maxHp);
        }

        private void OnDestroy()
        {
            FireSystem.Instance.HealthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}