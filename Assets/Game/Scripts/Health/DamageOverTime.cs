using System;
using Game.Scripts.LevelManager;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class DamageOverTime : MonoBehaviour
    {
        [SerializeField] private float damagePerSecond;
        [SerializeField] private HealthComponent healthComponent;

        private void Start()
        {
            Level.WinEvent += StopDamageOnWin;
        }

        private void Update()
        {
            healthComponent.TakeDamage(damagePerSecond * Time.deltaTime);
        }

        private void StopDamageOnWin()
        {
            enabled = false;
        }

        private void OnDestroy()
        {
            Level.WinEvent -= StopDamageOnWin;
        }
    }
}