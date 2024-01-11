using System;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.LevelManager
{
    public class Level : MonoBehaviour
    {
        public static event Action WinEvent;
        public static event Action LoseEvent;
        [SerializeField] private HealthComponent playerHealth;


        private void Start()
        {
            playerHealth.DeathEvent += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            if (LoseEvent != null) LoseEvent.Invoke();
        }

        private void OnDestroy()
        {
            playerHealth.DeathEvent -= OnPlayerDeath;
        }
    }
}