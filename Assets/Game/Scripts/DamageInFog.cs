using System;
using FOW;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(FogOfWarHider))]
    public class DamageInFog : MonoBehaviour
    {
        [SerializeField] private FogOfWarHider hider;
        [SerializeField] private DamageOverTime damageOverTime;

        private void Awake()
        {
            hider.OnActiveChanged += OnStatusChanged;
        }

        private void OnDestroy()
        {
            hider.OnActiveChanged -= OnStatusChanged;
        }

        private void OnStatusChanged(bool isActive)
        {
            damageOverTime.enabled = !isActive;
        }
    }
}