using System;
using DefaultNamespace;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts
{
    public class FireFx : MonoBehaviour
    {
        [SerializeField] private HealthComponent fireHealth;
        [SerializeField] private ParticleSystem mainFire;
        [SerializeField] private ParticleSystem embersSmall;
        [SerializeField] private ParticleSystem glow;
        [SerializeField] private ParticleSystem embersFlickering;
        [SerializeField] private AnimationCurve mainFireSizeCurve;
        [SerializeField] private AnimationCurve glowSizeCurve;
        [SerializeField] private AnimationCurve embersFlickeringCurve;


        private void Start()
        {
            fireHealth.HealthChangeEvent += OnHealthChanged;
        }

        private void OnHealthChanged(float currentHp, float maxHp)
        {
            float t = currentHp / maxHp;
            //mainFire
            var mainModule = mainFire.main;
            float mainFireSize = mainFireSizeCurve.Evaluate(t);
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainFireSize);

            //backFire
            mainModule = embersSmall.main;
            const float backFireSizeMultiplier = 1.1f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainFireSize * backFireSizeMultiplier);
            
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
            fireHealth.HealthChangeEvent -= OnHealthChanged;
        }
    }
}