using UnityEngine;

namespace Game.Scripts.Health
{
    public class HealingOverTime : MonoBehaviour
    {
        [SerializeField] private float healPerSecond;
        [SerializeField] private HealthComponent healthComponent;

        private void Update()
        {
            healthComponent.Healing(healPerSecond * Time.deltaTime);
        }
    }
}