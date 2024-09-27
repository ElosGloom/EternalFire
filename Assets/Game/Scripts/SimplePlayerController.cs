using System;
using Game.Scripts.Chest;
using Game.Scripts.GUI;
using UnityEngine;

namespace Game.Scripts
{
    public class SimplePlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float boostedSpeed;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator animator;
        private static readonly int Run = Animator.StringToHash("Run");
        private float _cashedSpeed;

        private void Start()
        {
            MovementSpeedBuff.BuffActivatedEvent += BoostSpeed;
            UIBuff.BuffEndEvent += SpeedBuffEnd;
            _cashedSpeed = speed;
        }

        private void SpeedBuffEnd()
        {
            speed = _cashedSpeed;
        }

        private void BoostSpeed()
        {
            speed = boostedSpeed;
        }

        private void FixedUpdate()
        {
            var isJoystickActive = MainJoystick.Instance.IsActive();
            if (isJoystickActive)
                rb.velocity = MainJoystick.Instance.Direction * speed;

            animator.SetBool(Run, isJoystickActive);
            if (rb.velocity.magnitude < 0.1f)
                return;

            var targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 0.1f));
        }

        private void OnDestroy()
        {
            MovementSpeedBuff.BuffActivatedEvent -= BoostSpeed;
            UIBuff.BuffEndEvent -= SpeedBuffEnd;
        }
    }
}