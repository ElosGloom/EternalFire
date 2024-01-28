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

            trail.enabled = true;
            animator.SetTrigger(TreeCut);
            StartCoroutine(CooldownResetRoutine());
            StartCoroutine(DamageDelayRoutine(other));
            
        }

        private IEnumerator CooldownResetRoutine()
        {
            yield return new WaitForFixedUpdate();
            _nextAttackTimeLeft = attackCooldown;
        }

        private IEnumerator DamageDelayRoutine(Collider other)
        {
            yield return new WaitForSeconds(damageDelay);
            if (other)
            {
                SfxController.PlayRandomSfx(0.2f, "event:/Chop1", "event:/Chop2");
                vfx.Play();
                other.GetComponent<HealthComponent>().TakeDamage(damage);
            }
        }
    }
}