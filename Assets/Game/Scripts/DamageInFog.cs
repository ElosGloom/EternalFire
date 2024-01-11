using FOW;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(FogOfWarHider))]
    public class DamageInFog : HiderBehavior
    {
        [SerializeField] private DamageOverTime damageOverTime;

        protected override void OnReveal()
        {
            damageOverTime.enabled = false;
        }

        protected override void OnHide()
        {
            damageOverTime.enabled = true;
        }
    }
}