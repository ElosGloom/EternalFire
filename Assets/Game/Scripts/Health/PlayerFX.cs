using FoW;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class PlayerFX : HiderBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private ParticleSystem damageVFX;
        [SerializeField] private ParticleSystem healVFX;


        private void Start()
        {
            healthComponent.DeathEvent += OnDeath;
            healthComponent.HealthChangeEvent += OnHealthChange;
        }

        public override void OnVisionStatusChanged(bool isVisible)
        {
            if (isVisible)
            {
                if (healthComponent.CurrentHealth < healthComponent.MaxHealth)
                {
                    healVFX.Play();
                }
                damageVFX.Stop();
                
            }
            else
            {
                healVFX.Stop();
                damageVFX.Play();
            }
        }

        private void OnHealthChange()
        {
           
            if (healthComponent.CurrentHealth >= healthComponent.MaxHealth)
            {
                healVFX.Stop();
            }
        }

        private void OnDeath()
        {
            damageVFX.Stop();
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChange;
            healthComponent.DeathEvent -= OnDeath;
        }
    }
}