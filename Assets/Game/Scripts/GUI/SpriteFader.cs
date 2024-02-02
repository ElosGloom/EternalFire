using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Game.Scripts.GUI
{
    public class SpriteFader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup damageFrame;
        [SerializeField] private float hitMoveBackDuration = 0.1f;
        [SerializeField] private float hitReturnToPlaceDuration = 0.4f;
        [SerializeField] private float hitImageAlpha = 0.1f;
        private Sequence _mySequence;


        public void Fading()
        {
            _mySequence = DOTween.Sequence();
            _mySequence.AppendInterval(0.35f);
            _mySequence.Append(damageFrame.DOFade(hitImageAlpha, hitMoveBackDuration));
            _mySequence.Append(damageFrame.DOFade(0f, hitReturnToPlaceDuration));
            _mySequence.SetLoops(-1);
            _mySequence.Play();
        }

        public void StopFading()
        {
            _mySequence.Kill();
            damageFrame.DOFade(0f, hitReturnToPlaceDuration);
        }

        private void OnDestroy()
        {
            _mySequence.Kill();
        }
    }
}