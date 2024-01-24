using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Inventory;
using UnityEngine;

namespace FPS.Pool
{
    public class PoolExample : MonoBehaviour
    {
        private HashSet<InventoryResource> t = new();
        private HashSet<InventoryResource> f = new();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                t.Add(FluffyPool.Get<InventoryResource>());
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                f.Add(FluffyPool.Get<InventoryResource>("coin"));
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