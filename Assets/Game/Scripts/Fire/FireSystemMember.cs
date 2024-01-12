using FOW;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireSystemMember : MonoBehaviour
    {
        [SerializeField] private FogOfWarRevealer3D fogOfWarRevealer;
        [SerializeField] private AnimationCurve lightRadiusCurve;


        private void Awake()
        {
            var healthComponent = FireSystem.Instance.HealthComponent;
            OnHealthChanged(healthComponent.CurrentHealth, healthComponent.MaxHealth);
            healthComponent.HealthChangeEvent += OnHealthChanged;
        }


        private void OnHealthChanged(float currentHp, float maxHp)
        {
            float t = currentHp / maxHp;
            fogOfWarRevealer.ViewRadius = lightRadiusCurve.Evaluate(t);
        }

        private void OnDestroy()
        {
            FireSystem.Instance.HealthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}