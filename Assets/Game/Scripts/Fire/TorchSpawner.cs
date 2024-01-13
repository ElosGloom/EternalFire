using UnityEngine;

namespace Game.Scripts.Fire
{
    public class TorchSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject torchPrefab;
        [SerializeField] private Transform fireParent;
        private float _minimalSpawnRadius = 5f;


        private void Update()
        {
            SearchingFire();
        }

        private void SearchingFire()
        {
            Vector3 nearestObject = FireSystem.Instance.SearchNearestMemberPosition(transform.position);

            if (Vector3.Distance(transform.position, nearestObject) >= _minimalSpawnRadius)
            {
                Vector3 playerPosition = transform.position;
                Instantiate(torchPrefab, playerPosition, Quaternion.identity, fireParent);
            }
        }
    }
}