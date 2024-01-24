using UnityEngine;

namespace FPS.FoW
{
    public class FogRevealer : MonoBehaviour
    {
        [SerializeField] private float radius = 10;
        [SerializeField, Range(0.01f, 1)] public float revealForce = 1;

        private Transform _cachedTransform;

        public Vector3 Position => _cachedTransform.position;

        public float Radius
        {
            get => radius;
            set
            {
                if (value < 0)
                    value = 0;

                radius = value;
            }
        }

        public float RevealForce
        {
            get => revealForce;
            set => revealForce = Mathf.Clamp(value, 0.01f, 1f);
        }

        private void Awake()
        {
            _cachedTransform = transform;
        }

        private void OnEnable()
        {
            FogOfWar.AddRevealer(this);
        }

        private void OnDisable()
        {
            FogOfWar.RemoveRevealer(this);
        }

        private void OnDestroy()
        {
            FogOfWar.RemoveRevealer(this);
        }
    }
}