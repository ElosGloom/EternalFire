using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPS.Pool
{
    public class PoolExample : MonoBehaviour
    {
        private HashSet<Transform> t = new();
        private HashSet<MeshFilter> f = new();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                t.Add(FluffyPool.Get<Transform>("t"));
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                f.Add(FluffyPool.Get<MeshFilter>("f"));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (t.Count > 0)
                {
                    var first = t.First();
                    t.Remove(first);
                    first.gameObject.SetActive(false);
                }

            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (f.Count > 0)
                {
                    var first = f.First();
                    f.Remove(first);
                    first.gameObject.SetActive(false);
                }
            }
        }
    }
}