using System;
using Game.Scripts.Fire;
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
            FireSystem.AllBonfiresConnectedEvent += OnAllBonfiresConnected;
        }

        private void OnPlayerDeath()
        {
            LoseEvent?.Invoke();
        }

        private void OnAllBonfiresConnected()
        {
            WinEvent?.Invoke();
        }

        private void OnDestroy()
        {
            playerHealth.DeathEvent -= OnPlayerDeath;
            FireSystem.AllBonfiresConnectedEvent -= OnAllBonfiresConnected;
        }
    }
}