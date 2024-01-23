using System;
using Game.Scripts.Fire;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class ArrowPointer : MonoBehaviour
    {
        private void Update()
        {
            NearestBonfire();
        }

        private void NearestBonfire()
        {
            var nearestBonfire = FireSystem.Instance.SearchNearestConnectedBonfire(transform.position);
            transform.LookAt(nearestBonfire.transform);
        }
    }
}