using System;
using System.Collections;
using Game.Scripts.Health;
using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts
{
    public class TreeCutter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private int damage;
        [SerializeField] private float damageDelay;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private TrailRenderer trail;
        [SerializeField] private CapsuleCollider treeCutterCollider;
        [SerializeField] private Transform colliderPoint0;
        [SerializeField] private Transform colliderPoint1;
        [SerializeField] private LayerMask mask;


        private float _nextAttackTimeLeft;
        private static readonly int TreeCut = Animator.StringToHash("TreeCut");


        private void Start()
        {
            _nextAttackTimeLeft = 0;
        }

        private void Update()
        {
            _nextAttackTimeLeft -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_nextAttackTimeLeft > 0)
                return;

            if (!other.gameObject.CompareTag("Tree"))
                return; //todo remove tag

            _nextAttackTimeLeft = attackCooldown;
            trail.enabled = true;
            animator.SetTrigger(TreeCut);
            StartCoroutine(DamageDelayRoutine());
        }

        private IEnumerator DamageDelayRoutine()
        {
            yield return new WaitForSeconds(damageDelay);
            var colliders = Physics.OverlapCapsule(colliderPoint0.position, colliderPoint1.position,
                treeCutterCollider.radius, mask,QueryTriggerInteraction.Ignore);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out HealthComponent healthComponent))
                {
                    AudioManager.Instance.PlayRandomSfx("Chop1","Chop2");
                    vfx.Play();
                    healthComponent.TakeDamage(damage);
                }
            }

            
            trail.enabled = false;
        }
    }
}