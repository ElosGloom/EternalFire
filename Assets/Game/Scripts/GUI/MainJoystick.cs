using System;
using UnityEngine;

namespace Game.Scripts.GUI
{
    [RequireComponent(typeof(Joystick))]
    public class MainJoystick : MonoBehaviour
    {
        public static MainJoystick Instance { get; private set; }
        [SerializeField] private Joystick joystick;
        private Camera _camera;


        public bool IsActive()
        {
            const float joystickThreshold = 0.15f;
            return joystick.Direction.magnitude > joystickThreshold && isActiveAndEnabled;
        }

        public Vector3 Direction => GetNormalizedWorldDirection();

        public void Awake()
        {
            _camera = Camera.main;
            Instance = this;
        }


        private Vector3 GetNormalizedWorldDirection()
        {
            float angle = _camera.transform.rotation.eulerAngles.y;
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            var sin = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 direction = joystick.Direction;
            float x = direction.x * cos - direction.y * sin;
            float z = direction.x * sin + direction.y * cos;

            return new Vector3(x, 0, z).normalized;
        }
    }
}