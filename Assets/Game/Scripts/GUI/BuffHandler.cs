using System;
using Game.Scripts.Chest;
using Game.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class BuffHandler : MonoBehaviour
    {
        [field: SerializeField] public SerializableDictionary<string, GameObject> Content { get; private set; }
        [SerializeField] private GameObject movementBuff;
        
        private void Start()
        {
            MovementSpeedBuff.BuffActivatedEvent += ShowUIBuff;
        }

        private void ShowUIBuff()
        {
            
            movementBuff.SetActive(true);
        }

        private void OnDestroy()
        {
            MovementSpeedBuff.BuffActivatedEvent -= ShowUIBuff;
        }
    }
}