using UnityEngine;

namespace Game.Scripts
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private void Update()
        {
            transform.position = target.transform.position;
        }
    }
}