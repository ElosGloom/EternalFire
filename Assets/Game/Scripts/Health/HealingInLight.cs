using FOW;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class HealingInLight : HiderBehavior
    {
        [SerializeField] private HealingOverTime healingOverTime;
        
        
        protected override void OnReveal()
        {
            healingOverTime.enabled = true;
        }

        protected override void OnHide()
        {
            healingOverTime.enabled = false;
        }
    }
}