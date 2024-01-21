using System.Collections.Generic;
using UnityEngine;

namespace FoW
{
    public class FogHider : MonoBehaviour, IHider
    {
        private readonly HashSet<IHiderBehaviour> _behaviours = new();
        private Transform _cachedTransform;
        private bool _lastVisibleStatus;
        public Vector3 Position => _cachedTransform.position;

        public void AddBehaviour(IHiderBehaviour behaviour) => _behaviours.Add(behaviour);

        private void OnVisibleStatusChanged()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.OnVisionStatusChanged(_lastVisibleStatus);
            }
        }

        private void Update()
        {
            bool isVisible = FogOfWar.IsHiderVisible(this);
            if (_lastVisibleStatus == isVisible)
                return;

            _lastVisibleStatus = isVisible;
            OnVisibleStatusChanged();
        }

        private void Awake()
        {
            _cachedTransform = transform;
        }
    }
}