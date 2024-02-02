using UnityEngine;

namespace Game.Scripts
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
        }

        private void LateUpdate()
        {
            _cachedTransform.position = target.position;
        }
    }
}