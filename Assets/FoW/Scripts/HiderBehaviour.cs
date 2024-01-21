using UnityEngine;

namespace FoW
{
    [RequireComponent(typeof(FogHider))]
    public abstract class HiderBehaviour : MonoBehaviour, IHiderBehaviour
    {
        private void Awake()
        {
            GetComponent<FogHider>().AddBehaviour(this);
        }

        public abstract void OnVisionStatusChanged(bool isVisible);
    }
}