using System;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireSystem : MonoBehaviour
    {
        public static FireSystem Instance { get; private set; }
        [SerializeField] private Bonfire[] bonfires;
        [SerializeField] private HealthComponent healthComponent;
        public Torch[] Torches;

        public HealthComponent HealthComponent => healthComponent;

        private void Awake()
        {
            Instance = this;
        }
    }
}