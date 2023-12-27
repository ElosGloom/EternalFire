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
    }
}