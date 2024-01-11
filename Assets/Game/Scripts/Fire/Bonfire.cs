using FOW;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class Bonfire : MonoBehaviour
    {
        [SerializeField] private HealthComponent _fireHealth;
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private float woodCooldown = 0.25f;
        [SerializeField] private float maxLightRadius = 5;
        [SerializeField] private FogOfWarRevealer3D fogOfWarRevealer;
        
        
        private float _triggerTimeLeft;
        private float _lightRadius;
        private float _waitTimeLeft;

        private void Start()
        {
            _fireHealth.HealthChangeEvent += OnHealthChanged;
        }

        private void Update()
        {
            _triggerTimeLeft -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_triggerTimeLeft < 0)
            {

                if (other.GetComponent<Inventory>() == null)
                    return;

                var canRemoveItem = _playerInventory.TryRemoveItem("Wood", 1);

                if (canRemoveItem)
                {
                    _triggerTimeLeft = woodCooldown;
                    _fireHealth.Healing(3);
                }
            }
        }

        private void OnHealthChanged(float currentHp, float maxHp)
        {
            fogOfWarRevealer.ViewRadius = _lightRadius;
            _lightRadius = Mathf.Lerp(0, maxLightRadius, currentHp / maxHp);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _lightRadius);
        }

        private void OnDestroy()
        {
            _fireHealth.HealthChangeEvent -= OnHealthChanged;
        }
    }
}