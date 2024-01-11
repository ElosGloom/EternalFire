using UnityEngine;

namespace Game.Scripts.Health
{
    public class DamageOverTime : MonoBehaviour
    {
        [SerializeField] private float damagePerSecond;
        [SerializeField] private HealthComponent healthComponent;


        private void Update()
        {
            healthComponent.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}