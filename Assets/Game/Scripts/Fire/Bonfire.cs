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
        [SerializeField] private FogOfWarRevealer3D fogOfWarRevealer;
        [SerializeField] private AnimationCurve lightRadiusCurve;

        private float _triggerTimeLeft;
        private float _waitTimeLeft;

        private void Awake()
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
            float t = currentHp / maxHp;
            fogOfWarRevealer.ViewRadius = lightRadiusCurve.Evaluate(t);
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, _lightRadius);
        // }

        private void OnDestroy()
        {
            _fireHealth.HealthChangeEvent -= OnHealthChanged;
        }
    }
}