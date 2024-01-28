using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class CurveMotion : MonoBehaviour
    {
        [SerializeField] private float height;
        [SerializeField] private float moveTime = 3f;


        public void Move(Vector3 from, Transform target, Action callback = null)
        {
            StartCoroutine(MoveRoutine(from, target, callback));
        }

        private IEnumerator MoveRoutine(Vector3 from, Transform target, Action callback = null)
        {
            float t = 0;
            while (t < 1)
            {
                var d = (target.position - from) / 2 + from;
                var c = new Vector3(d.x, d.y + height, d.z);

                var ac = Vector3.Lerp(from, c, t);
                var cb = Vector3.Lerp(c, target.position, t);
                var result = Vector3.Lerp(ac, cb, t);

                transform.position = result;
                t += Time.deltaTime / moveTime;
                yield return null;
            }

            callback?.Invoke();
        }
    }
}