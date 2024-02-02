using System;
using FPS.FoW;
using Game.Scripts.GUI;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class DamageInFog : HiderBehaviour
    {
        [SerializeField] private DamageOverTime damageOverTime;
        [SerializeField] private SpriteFader spriteFader;
        
        public override void OnVisionStatusChanged(bool isVisible)
        {
            if (!isVisible)
                spriteFader.Fading();
            else
                spriteFader.StopFading();
            
            damageOverTime.enabled = !isVisible;
        }
    }
}