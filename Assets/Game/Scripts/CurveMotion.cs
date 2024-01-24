using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class CurveMotion : MonoBehaviour
    {
        [SerializeField] private float height;

        public void Move(Vector3 from, Transform target)
        {
            StartCoroutine(MoveRoutine(from, target));
        }

        private IEnumerator MoveRoutine(Vector3 from, Transform target)
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
                yield return null;
                t += Time.deltaTime;
            }
        }
    }
}