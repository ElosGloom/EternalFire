using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireFx : MonoBehaviour
    {
        [SerializeField] private ParticleSystem mainFire;
        [SerializeField] private ParticleSystem glow;
        [SerializeField] private ParticleSystem embersFlickering;
        [SerializeField] private AnimationCurve mainFireSizeCurve;
        [SerializeField] private AnimationCurve glowSizeCurve;
        [SerializeField] private AnimationCurve embersFlickeringCurve;


        private void Start()
        {
            FireSystem.Instance.HealthComponent.HealthChangeEvent += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            float t = FireSystem.Instance.HealthComponent.CurrentHealth / FireSystem.Instance.HealthComponent.MaxHealth;
            //mainFire
            var mainModule = mainFire.main;
            float mainFireSize = mainFireSizeCurve.Evaluate(t);
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainFireSize);

            mainModule = glow.main;
            float glowSize = glowSizeCurve.Evaluate(t);
            const float glowSizeMultiplier = 1.1f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(glowSize * glowSizeMultiplier);

            mainModule = embersFlickering.main;
            float embersSmallSize = embersFlickeringCurve.Evaluate(t);
            mainModule.startSize = new ParticleSystem.MinMaxCurve(embersSmallSize);
        }

        private void OnDestroy()
        {
            FireSystem.Instance.HealthComponent.HealthChangeEvent -= OnHealthChanged;
        }
    }
}