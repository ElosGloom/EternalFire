using FPS.Pool;
using Game.Scripts.LevelManager;
using UnityEngine;

namespace Game.Scripts
{
    public class ReleaseOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private Poolable poolable;

        private void Awake()
        {
            Level.WinEvent += Release;
            Level.LoseEvent += Release;
        }

        private void OnDestroy()
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