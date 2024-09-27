using System.Collections;
using DG.Tweening;
using FPS.Pool;
using Game.Scripts.Inventory;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Chest
{
    public class Chest : MonoBehaviour
    {
        [field: SerializeField] public SerializableDictionary<ChestContent, GameObject> Content { get; private set; }

        [SerializeField] private float shakeDuration = 0.5f;
        [SerializeField] private float shakeStrength = 0.2f;
        [SerializeField] private float scaleDuration = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<TreeCutter>())
            {
                GiveRandomReward();
            }
        }

        private void GiveRandomReward()
        {
            var randomValue = Content.GetRandomKey();
            Content.TryGetValue(randomValue, out var value);
            if (value != null)
            {  var reward = FluffyPool.Get<InventoryResource>(randomValue.ToString());
                reward.transform.position = transform.position;
                reward.transform.SetParent(transform.parent);
                PlayPoofFX();
                DestroyChest();
            }
        }
        
        private void DestroyChest()
        {
            ShakeAndReduce();
            DOVirtual.DelayedCall(0.7f, () =>
            {
                Destroy(this.gameObject);
            });
            
        }

        private void ShakeAndReduce()
        {
            transform.DOShakeScale(shakeDuration, shakeStrength)
                .OnComplete(() =>
                {
                    transform.DOScale(Vector3.zero, scaleDuration);
                   
                });
        }

        private void PlayPoofFX()
        {
            var chestPoof = FluffyPool.Get<ParticleSystem>("ChestPoof");
            chestPoof.transform.position = transform.position;
            chestPoof.Play();
        }
    }
}