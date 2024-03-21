using System;
using Game.Scripts.Fire;
using Game.Scripts.Health;
using Game.Scripts.SFX;
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
            AudioManager.Instance.PlaySfx("Lose");
            LoseEvent?.Invoke();
        }

        private void OnAllBonfiresConnected()
        {
           AudioManager.Instance.PlaySfx("LevelComplete");
            WinEvent?.Invoke();
        }

        private void OnDestroy()
        {
            playerHealth.DeathEvent -= OnPlayerDeath;
            FireSystem.AllBonfiresConnectedEvent -= OnAllBonfiresConnected;
        }
    }
}