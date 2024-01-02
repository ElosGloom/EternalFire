using System;
using DefaultNamespace;
using UnityEngine;

namespace Game.Scripts
{
    public class FireFx : MonoBehaviour
    {
        [SerializeField] private HealthComponent fireHealth;
        [SerializeField] private ParticleSystem mainFire;
        [SerializeField] private ParticleSystem backFire;
        [SerializeField] private ParticleSystem glow;
        [SerializeField] private AnimationCurve mainFireSizeCurve;


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
            mainModule = backFire.main;
            const float backFireSizeMultiplier = 1.1f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainFireSize * backFireSizeMultiplier);
            
            mainModule = glow.main;
            const float glowSizeMultiplier = 1.1f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainFireSize * glowSizeMultiplier);
            
        }

        private void OnDestroy()
        {
            fireHealth.HealthChangeEvent -= OnHealthChanged;
        }
    }
}