using UnityEngine;

namespace Game.Scripts.Fire
{
    public class Bonfire : FireSystemMember
    {
        [SerializeField] private float woodCooldown = 0.25f;
        [SerializeField] private GameObject revealer;
        
        
        private float _triggerTimeLeft;
        private float _waitTimeLeft;


        private void Update()
        {
            _triggerTimeLeft -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_triggerTimeLeft < 0)
            {
                if (!other.TryGetComponent(out Inventory.Inventory inventory))
                {
                    return;
                }


                var canRemoveItem = inventory.TryRemoveItem("Wood", 1);

                if (canRemoveItem)
                {
                    _triggerTimeLeft = woodCooldown;
                    FireSystem.Instance.HealthComponent.Healing(3);
                }
            }
        }

        public void SwitchActive(bool value)
        {
           revealer.SetActive(value);
        }
    }
}