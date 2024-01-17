using FOW;
using UnityEngine;

namespace Game.Scripts.Health
{
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