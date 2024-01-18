using System;
using FOW;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class DamageInFog : HiderBehavior
    {
        [SerializeField] private DamageOverTime damageOverTime;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField, Min(0.1f)] private float fadeTime = 1;
        private bool _inFog;

        protected override void OnReveal()
        {
            _inFog = false;
            damageOverTime.enabled = false;
        }

        protected override void OnHide()
        {
            _inFog = true;
            damageOverTime.enabled = true;
        }

        private void Update()
        {
            if (_inFog)
                canvasGroup.alpha += Time.deltaTime / fadeTime;
            else
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
        }
    }
}