using FPS.FoW;
using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class PlayerFX : HiderBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private ParticleSystem damageVFX;
        [SerializeField] private ParticleSystem healVFX;
        [SerializeField] private ParticleSystem woodPoof;
        [SerializeField] private TrailRenderer axeTrail;


        private void Start()
        {
            healthComponent.DeathEvent += OnDeath;
            healthComponent.HealthChangeEvent += OnHealthChange;
            TreeCutter.StartTreeCuttingEvent += OnTreeCuttingStart;
            TreeCutter.TreeChoppingEvent += OnTreeChopping;
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

        private void OnTreeCuttingStart()
        {

            axeTrail.enabled = true;
        }

        private void OnTreeChopping()
        {
            woodPoof.Play();
            axeTrail.enabled = false;
            AudioManager.Instance.PlayRandomSfx("Chop1", "Chop2");
        }

        private void OnDeath()
        {
            damageVFX.Stop();
        }

        private void OnDestroy()
        {
            healthComponent.HealthChangeEvent -= OnHealthChange;
            healthComponent.DeathEvent -= OnDeath;
            TreeCutter.StartTreeCuttingEvent -= OnTreeCuttingStart;
            TreeCutter.TreeChoppingEvent -= OnTreeChopping;
        }
    }
}