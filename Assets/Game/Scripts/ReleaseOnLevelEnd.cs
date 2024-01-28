using FPS.Pool;
using Game.Scripts.LevelManager;
using UnityEngine;

namespace Game.Scripts
{
    public class ReleaseOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private Poolable poolable;

        private void OnEnable()
        {
            Level.WinEvent += Release;
            Level.LoseEvent += Release;
        }

        private void OnDisable()
        {
            Level.WinEvent -= Release;
            Level.LoseEvent -= Release;
        }

        private void Release()
        {
            poolable.Release();
        }

        private void OnValidate()
        {
            poolable ??= GetComponent<Poolable>();
        }
    }
}