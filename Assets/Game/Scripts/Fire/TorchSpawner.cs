using System;
using System.Collections;
using FOW;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class TorchSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject torchPrefab;
        [SerializeField] private Transform fireParent;
        private float _searchRadius = 5f;

        private void Update()
        {
            SearchingFire();
        }

        private void SearchingFire()
        {
            FogOfWarRevealer3D[] allObjects = FindObjectsOfType<FogOfWarRevealer3D>();

            FogOfWarRevealer3D nearestObject = null;
            float nearestDistance = Mathf.Infinity;


            foreach (FogOfWarRevealer3D obj in allObjects)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < nearestDistance)
                {
                    nearestObject = obj;
                    nearestDistance = distance;
                }
            }

            if (nearestObject != null)
            {
                if (Vector3.Distance(transform.position, nearestObject.transform.position) >= _searchRadius)
                {
                    Vector3 playerPosition = transform.position;
                    Instantiate(torchPrefab, playerPosition, Quaternion.identity, fireParent);
                }
            }
        }
    }
}