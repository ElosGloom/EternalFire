using System;
using System.Collections;
using FOW;
using UnityEngine;

namespace Game.Scripts.Fire
{
    public class TorchSpawner : HiderBehavior
    {
        [SerializeField] private GameObject torchPrefab;
        [SerializeField] private Transform fireParent;


        private IEnumerator SpawnTorch()
        {
            yield return null;
            Vector3 playerPosition = transform.position;
            Instantiate(torchPrefab, playerPosition, Quaternion.identity, fireParent);
        }

        protected override void OnReveal()
        {
        }

        protected override void OnHide()
        {
            StartCoroutine(SpawnTorch());
        }
    }
}