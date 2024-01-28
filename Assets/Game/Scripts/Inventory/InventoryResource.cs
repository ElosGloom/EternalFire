using System;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    public class InventoryResource : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private int itemsCount = 1;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider itemCollider;

        public CurveMotion curveMotion;

        public string ItemName => itemName;
        public int ItemsCount => itemsCount;
        public Rigidbody Rigidbody => rb;
        public Collider ItemCollider => itemCollider;
    }
}