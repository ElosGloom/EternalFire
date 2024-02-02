using UnityEngine;
using DG.Tweening;
using Game.Scripts.Health;

namespace Game.Scripts.GUI
{
    public class FireIconBounce : MonoBehaviour
    {
        [SerializeField] private GameObject fireIcon;
        [SerializeField] private HealthComponent fireHealth;


        private void Start()
        {
            fireHealth.HealthChangeEvent += ApplyBounceEffect;
        }

        private void ApplyBounceEffect()
        {
            if (fireHealth.CurrentHealth <= fireHealth.MaxHealth / 3)
            {
                if (!DOTween.IsTweening(fireIcon.transform))
                {
                    fireIcon.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 2, 1f);
                }
            }
        }

        private void OnDestroy()
        {
            fireHealth.HealthChangeEvent -= ApplyBounceEffect;
        }
    }
}