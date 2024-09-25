using System;
using System.Collections.Generic;
using Game.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public static class Extensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        public static TKey GetRandomKey<TKey, TValue>(this SerializableDictionary<TKey, TValue> dictionary)
        {
            TKey[] keys = new TKey[dictionary.Count];
            dictionary.Keys.CopyTo(keys, 0);

            int randomIndex = Random.Range(0, keys.Length);
            return keys[randomIndex];
        }

        public static T GetRandomElement<T>(this T[] list)
        {
            int randomIndex = Random.Range(0, list.Length);
            return list[randomIndex];
        }

        public static T GetNearestObject<T>(this IEnumerable<T> objects, Vector3 position) where T : Component
        {
            T nearestObject = default;
            var nearestDistance = float.MaxValue;

            foreach (var item in objects)
            {
                var distance = Vector3.Distance(item.transform.position, position);
                if (distance > nearestDistance)
                    continue;

                nearestDistance = distance;
                nearestObject = item;
            }

            return nearestObject;
        }
    }
}