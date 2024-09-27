using System;
using UnityEngine;

namespace Game.Scripts.Chest
{
    public class MovementSpeedBuff : MonoBehaviour
    {
        public static event Action BuffActivatedEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SimplePlayerController>())
            {
                BuffActivate();
                Destroy(gameObject);
            }
        }

        private void BuffActivate()
        {
            BuffActivatedEvent?.Invoke();
        }
    }
}