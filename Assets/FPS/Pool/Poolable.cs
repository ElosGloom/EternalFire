using System;
using UnityEngine;

namespace FPS.Pool
{
    public class Poolable : MonoBehaviour
    {
        public static event Action<Component> ReleaseEvent;
        [field: SerializeField, ComponentSelector] public Component Type { get; private set; }

        private void OnDisable()
        {
            ReleaseEvent?.Invoke(Type);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Type ??= transform;
            // string goName = gameObject.name;
            // if (goName.Contains('>'))
            // {
            //     goName = goName.Split('>')[^1];
            // }
            // gameObject.name = $"<{Type.GetType().Name}>{goName}";
        }
  #endif
    }
}