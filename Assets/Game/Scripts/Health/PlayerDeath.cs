using System;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private HealthComponent health;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private Animator animator;
        [SerializeField] private TreeCutter treeCutter;
        
        private static readonly int DeathHash = Animator.StringToHash("Death");
        
        
        private void Start()
        {
            health.DeathEvent += Death;
        }

        private void Death()
        {
            treeCutter.enabled = false;
            animator.SetTrigger(DeathHash);
            vfx.Play();
        }

        private void OnDestroy()
        {
            health.DeathEvent -= Death;
        }
    }
}