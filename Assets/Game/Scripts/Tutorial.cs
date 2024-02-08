using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Game.Scripts
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Transform arrow;
        [SerializeField] private Transform tree;
        [SerializeField] private Transform activeBonfire;
        [SerializeField] private Transform inactiveBonfire;
        [SerializeField] private Transform player;


        private Transform _currentTarget;
        private Sequence _arrowAnimation;

        private void Start()
        {
            _currentTarget = tree;
            SetArrowTarget(_currentTarget);
        }

        private void Update()
        {
            if (_currentTarget != null &&
                Vector3.Distance(player.position, _currentTarget.position) < 1.5f)
            {
                enabled = false;
                StartCoroutine(TargetChangeDelayRoutine());
            }
        }

        private IEnumerator TargetChangeDelayRoutine()
        {
            yield return new WaitForSeconds(2.5f);

            enabled = true;
            if (_currentTarget == tree)
            {
                _currentTarget = activeBonfire;
            }
            else if (_currentTarget == activeBonfire)
            {
                _currentTarget = inactiveBonfire;
            }
            else if (_currentTarget == inactiveBonfire)
            {
                _currentTarget = null;
                SetArrowTarget(null);
            }

            SetArrowTarget(_currentTarget);
        }

        private void SetArrowTarget(Transform target)
        {
            if (_arrowAnimation != null)
            {
                _arrowAnimation.Kill();
            }


            if (target != null)
            {
                arrow.gameObject.SetActive(true);
                var arrowPos = target.position + new Vector3(0, 1.5f, 0);
                arrow.position = arrowPos;

                _arrowAnimation = DOTween.Sequence();
                _arrowAnimation.Append(arrow.DOMoveY(arrowPos.y + 2f, 1f).SetEase(Ease.InOutSine));
                _arrowAnimation.Append(arrow.DOMoveY(arrowPos.y, 1f).SetEase(Ease.InOutSine));
                _arrowAnimation.SetLoops(-1);
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}