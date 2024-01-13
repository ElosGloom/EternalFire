using System;
using System.Collections.Generic;
using Game.Scripts.Health;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireSystem : MonoBehaviour
    {
        public static FireSystem Instance { get; private set; }
        [SerializeField] private Bonfire[] bonfires;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private List<Torch> _torches;
        private FireSystemMember[] _connectedMembers;


        public HealthComponent HealthComponent => healthComponent;

        private void Awake()
        {
            Instance = this;
            _torches = new List<Torch>();
        }

        public bool TryUnregisterLastTorch(out Torch lastTorch)
        {
            if (_torches.Count > 0)
            {
                lastTorch = _torches[_torches.Count - 1];
                _torches.RemoveAt(_torches.Count - 1);
                return true;
            }

            lastTorch = null;
            return false;
        }

        public void RegisterNewTorch(Torch newTorch)
        {
            _torches.Add(newTorch);
        }
    }
}