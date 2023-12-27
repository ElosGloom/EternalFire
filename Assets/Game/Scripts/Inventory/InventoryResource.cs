using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class InventoryResource : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private int itemsCount = 1;
        [SerializeField] private Rigidbody rb;

        public string ItemName => itemName;

        public int ItemsCount => itemsCount;
    }
}