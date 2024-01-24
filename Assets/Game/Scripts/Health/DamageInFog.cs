using FPS.FoW;
using UnityEngine;

namespace Game.Scripts.Health
{
    public class DamageInFog : HiderBehaviour
    {
        [SerializeField] private DamageOverTime damageOverTime;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField, Min(0.1f)] private float fadeTime = 1;
        
        private bool _inFog;
        
        private void Update()
        {
            if (_inFog)
                canvasGroup.alpha += Time.deltaTime / fadeTime;
            else
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
        }

        public override void OnVisionStatusChanged(bool isVisible)
        {
            _inFog = !isVisible;
            damageOverTime.enabled = !isVisible;
        }
    }
}