using FPS.FoW;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class HealingInLight : HiderBehaviour
    {
        [SerializeField] private HealingOverTime healingOverTime;

        public override void OnVisionStatusChanged(bool isVisible)
        {
            healingOverTime.enabled = isVisible;
        }
    }
}