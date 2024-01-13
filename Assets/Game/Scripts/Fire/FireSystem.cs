using System.Collections.Generic;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class FireSystem : MonoBehaviour
    {
        public static FireSystem Instance { get; private set; }

        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private List<FireSystemMember> connectedMembers;

        private List<Torch> _torches;
        // [SerializeField] private Bonfire[] bonfires;


        public HealthComponent HealthComponent => healthComponent;

        private void Awake()
        {
            Instance = this;
            _torches = new List<Torch>();
        }

        public Vector3 SearchNearestMemberPosition(Vector3 spawnerPosition)
        {
            FireSystemMember nearestObject = null;
            float nearestDistance = Mathf.Infinity;


            foreach (FireSystemMember member in connectedMembers)
            {
                float distance = Vector3.Distance(spawnerPosition, member.transform.position);

                if (distance < nearestDistance)
                {
                    nearestObject = member;
                    nearestDistance = distance;
                }
            }

            return nearestObject.transform.position;
        }

        public bool TryDisconnectLastTorch(out Torch lastTorch)
        {
            if (_torches.Count > 0)
            {
                lastTorch = _torches[_torches.Count - 1];
                _torches.RemoveAt(_torches.Count - 1);
                connectedMembers.RemoveAt(connectedMembers.Count - 1);
                return true;
            }

            lastTorch = null;
            return false;
        }

        private void ConnectMember(FireSystemMember newMember)
        {
            connectedMembers.Add(newMember);
        }


        public void ConnectNewTorch(Torch newTorch)
        {
            _torches.Add(newTorch);
            ConnectMember(newTorch);
        }
    }
}