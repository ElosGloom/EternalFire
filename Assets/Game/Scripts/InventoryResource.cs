using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class InventoryResource : MonoBehaviour
    {
        private float destroyDelay = 3f;
        [SerializeField] private string itemName;
        [SerializeField] private int itemsCount = 1;

        public string ItemName => itemName;

        public int ItemsCount => itemsCount;


        // private void OnTriggerEnter(Collider other)
        // {
        //     Destroy(gameObject);
        //     // StartCoroutine(DestroyDelayRoutine());
        // }
        //
        // private IEnumerator DestroyDelayRoutine()
        // {
        //     yield return new WaitForSeconds(destroyDelay);
        //
        //   
        // }
    }
}