using System;
using System.Collections.Generic;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireSystem : MonoBehaviour
    {
        public static FireSystem Instance { get; private set; }
        public static event Action AllBonfiresConnectedEvent;
        public static event Action InactiveBonfiresCountChangeEvent;
        public static event Action<bool> HasTorchesToReturn;

        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private List<FireSystemMember> connectedMembers;
        [SerializeField] private ParticleSystem fireConnectionFX;
        [SerializeField] private List<Bonfire> inactiveBonfires;
        [SerializeField] private List<Bonfire> activeBonfires;


        private static List<Torch> _torches;
        public HealthComponent HealthComponent => healthComponent;


        private void Awake()
        {
            foreach (var bonfire in inactiveBonfires)
            {
                bonfire.SwitchActive(false);
            }

            Instance = this;
            _torches = new List<Torch>();
        }

        private void Start()
        {
            HasTorchesToReturn?.Invoke(false);
        }


        public FireSystemMember SearchNearestMember(Vector3 spawnerPosition)
        {
            return connectedMembers.GetNearestObject(spawnerPosition);
        }

        public FireSystemMember SearchNearestConnectedBonfire(Vector3 spawnerPosition)
        {
            return activeBonfires.GetNearestObject(spawnerPosition);
        }


        public bool TryDisconnectLastTorch(out Torch lastTorch)
        {
            if (_torches.Count > 0)
            {
                lastTorch = _torches[_torches.Count - 1];
                _torches.RemoveAt(_torches.Count - 1);
                connectedMembers.RemoveAt(connectedMembers.Count - 1);
                HasTorchesToReturn?.Invoke(_torches.Count > 0);

                return true;
            }

            lastTorch = null;
            return false;
        }

        private void ConnectMember(FireSystemMember newMember, FireSystemMember nearestMember)
        {
            connectedMembers.Add(newMember);
            var nearestMemberTransform = nearestMember.transform;
            var fx = Instantiate(fireConnectionFX, nearestMemberTransform.position, Quaternion.identity,
                newMember.transform);

            fx.externalForces.AddInfluence(newMember.ForceField);
        }

        public void ConnectNewTorch(Torch newTorch, FireSystemMember nearestMember)
        {
            _torches.Add(newTorch);
            HasTorchesToReturn?.Invoke(_torches.Count > 0);
            ConnectMember(newTorch, nearestMember);
        }

        public void TryConnectBonfire(float maxSpawnRadius, FireSystemMember connectionProvider)
        {
            for (int i = 0; i < inactiveBonfires.Count; i++)
            {
                var bonfireDistance = Vector3.Distance(
                    connectionProvider.transform.position,
                    inactiveBonfires[i].transform.position);

                if (bonfireDistance > maxSpawnRadius)
                    continue;

                ConnectMember(inactiveBonfires[i], connectionProvider);
                activeBonfires.Add(inactiveBonfires[i]);
                inactiveBonfires[i].SwitchActive(true);
                inactiveBonfires.RemoveAt(i);
                InactiveBonfiresCountChangeEvent?.Invoke();
                if (inactiveBonfires.Count == 0)
                {
                    AllBonfiresConnectedEvent?.Invoke();
                }

                _torches.Clear();
                HasTorchesToReturn?.Invoke(_torches.Count > 0);
            }
        }

        public int GetInactiveBonfiresCount()
        {
            return inactiveBonfires.Count;
        }
    }
}