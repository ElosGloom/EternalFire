using UnityEngine;

namespace FOW
{
    [RequireComponent(typeof(FogOfWarHider))]
    public abstract class HiderBehavior : MonoBehaviour
    {
        protected bool IsEnabled;
        [SerializeField] private FogOfWarHider fogOfWarHider;

        protected virtual void Awake()
        {
            OnHide();
            fogOfWarHider.OnActiveChanged += OnStatusChanged;
        }

        private void OnValidate()
        {
            if (fogOfWarHider == null)
                fogOfWarHider = GetComponent<FogOfWarHider>();
           
        }

        void OnStatusChanged(bool isEnabled)
        {
            IsEnabled = isEnabled;
            if (isEnabled)
                OnReveal();
            else
                OnHide();
        }

        private void OnDestroy()
        {
            fogOfWarHider.OnActiveChanged -= OnStatusChanged;
        }

        protected abstract void OnReveal();
        protected abstract void OnHide();
    }
}