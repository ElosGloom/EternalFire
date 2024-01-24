using System;
using Game.Scripts.Fire;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class ArrowPointer : MonoBehaviour
    {
        [SerializeField] private HealthComponent playerHealth;
        
        private void Start()
        {
            playerHealth.DeathEvent += DisableOnDeath;
        }

        private void Update()
        {
            NearestBonfire();
        }

        private void NearestBonfire()
        {
            var nearestBonfire = FireSystem.Instance.SearchNearestConnectedBonfire(transform.position);
            transform.LookAt(nearestBonfire.transform);
        }

        private void DisableOnDeath()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            playerHealth.DeathEvent -= DisableOnDeath;
        }
    }
}