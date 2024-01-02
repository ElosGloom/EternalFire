using System;
using DefaultNamespace;
using UnityEngine;

namespace Game.Scripts
{
    public class Bonfire : MonoBehaviour

    {
        [SerializeField] private HealthComponent _fireHealth;
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private float cooldown = 1f;
        private float _waitTimeLeft;
        [SerializeField] private float woodCooldown = 0.25f;
        private float _triggerTimeLeft;
        private float _lightRadius;
        [SerializeField] private float maxLightRadius=5;
        

        private void Start()
        {
            _fireHealth.HealthChangeEvent += OnHealthChanged;
        }

        private void Update()
        {
            _waitTimeLeft -= Time.deltaTime;
            _triggerTimeLeft -= Time.deltaTime;

            if (_waitTimeLeft < 0)
            {
                _waitTimeLeft = cooldown;
                _fireHealth.TakeDamage(1);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_triggerTimeLeft < 0)
            {
              
                if (other.GetComponent<Inventory>() == null)
                    return;

                var canRemoveItem= _playerInventory.TryRemoveItem("Wood", 1);
                
                if (canRemoveItem)
                {
                    _triggerTimeLeft = woodCooldown;
                    _fireHealth.Healing(3);
                }
            }
        }

        private void OnHealthChanged(float currentHp, float maxHp)
        {
            _lightRadius = Mathf.Lerp(0, maxLightRadius, currentHp / maxHp);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,_lightRadius);
        }

        private void OnDestroy()
        {
            _fireHealth.HealthChangeEvent -= OnHealthChanged;
        }
    }
}