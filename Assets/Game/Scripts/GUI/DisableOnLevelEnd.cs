using System;
using Game.Scripts.LevelManager;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class DisableOnLevelEnd : MonoBehaviour
    {
        private void Start()
        {
            Level.LoseEvent += Disable;
            Level.WinEvent += Disable;
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Level.WinEvent -= Disable;
            Level.LoseEvent -= Disable;
        }
    }
}