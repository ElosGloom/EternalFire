using UnityEngine;

namespace FoW
{
    public class SwitchGameObjectBehaviour : HiderBehaviour
    {
        [SerializeField] private GameObject go;

        public override void OnVisionStatusChanged(bool isVisible)
        {
            go.SetActive(isVisible);
        }
    }
}