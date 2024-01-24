using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPS.Pool
{
    public class FluffyPool : MonoBehaviour
    {
        private static FluffyPool _instance;

        private static readonly HashSet<Component> ActivePoolablesByType = new();
        private static readonly Dictionary<Component, string> ActivePoolablesByString = new();

        private static readonly Dictionary<Type, Component> PrefabsByType = new();
        private static readonly Dictionary<string, Component> PrefabsByString = new();
        private static readonly Dictionary<Type, Transform> ParentsByType = new();
        private static readonly Dictionary<string, Transform> ParentsByString = new();

        private static readonly Dictionary<Type, HashSet<Component>> InactivePoolablesByType = new();
        private static readonly Dictionary<string, HashSet<Component>> InactivePoolablesByString = new();

        [SerializeField] private PoolDescription[] poolDescription = { new(1) };

        public static T Get<T>() where T : Component
        {
            var type = typeof(T);
            if (InactivePoolablesByType.TryGetValue(type, out var pool))
            {
                Component poolable;
                if (pool.Count > 0)
                {
                    poolable = pool.First();
                    pool.Remove(poolable);
                    ActivePoolablesByType.Add(poolable);
                    poolable.gameObject.SetActive(true);
                }
                else
                    poolable = CreateNew(type);

                return poolable as T;
            }

            throw new Exception($"FluffyPool: Pool with type {type} does not exist");
        }

        public static T Get<T>(string key) where T : Component
        {
            if (InactivePoolablesByString.TryGetValue(key, out var pool))
            {
                Component poolable;
                if (pool.Count > 0)
                {
                    poolable = pool.First();
                    pool.Remove(poolable);
                    ActivePoolablesByString.Add(poolable, key);
                    poolable.gameObject.SetActive(true);
                }
                else
                    poolable = CreateNew(key);

                if (poolable is T component)
                    return component;

                throw new Exception($"FluffyPool: Poolable with key {key} is not {typeof(T)} ");
            }

            throw new Exception($"FluffyPool: Pool with key \"{key}\" does not exist");
        }

        private static Component CreateNew(Type type)
        {
            var newPoolable = Instantiate(PrefabsByType[type], ParentsByType[type]);
            ActivePoolablesByType.Add(newPoolable);
            return newPoolable;
        }

        private static Component CreateNew(string key)
        {
            var newPoolable = Instantiate(PrefabsByString[key], ParentsByString[key]);
            ActivePoolablesByString.Add(newPoolable, key);
            return newPoolable;
        }

        private static void Return(Component poolable)
        {
            if (ActivePoolablesByString.ContainsKey(poolable))
            {
                string key = ActivePoolablesByString[poolable];
                InactivePoolablesByString[key].Add(poolable);
                ActivePoolablesByString.Remove(poolable);
                poolable.transform.SetParent(ParentsByString[key]);
            }
            else if (ActivePoolablesByType.Contains(poolable))
            {
                var type = poolable.GetType();
                InactivePoolablesByType[type].Add(poolable);
                ActivePoolablesByType.Remove(poolable);
                poolable.transform.SetParent(ParentsByType[type]);
            }
            else
                Debug.LogWarning($"FluffyPool: Can't return \"{poolable.gameObject.name}\", because it hasn't been registered before");
        }

        private void Init()
        {
            foreach (var description in poolDescription)
            {
                if (string.IsNullOrEmpty(description.Key))
                {
                    var type = description.Prefab.Type.GetType();
                    InactivePoolablesByType.Add(type, new HashSet<Component>(description.Count * 2));
                    var parent = new GameObject($"<{type.Name}>").transform;
                    parent.SetParent(_instance.transform);
                    ParentsByType.Add(type, parent);

                    if (!PrefabsByType.TryAdd(type, description.Prefab.Type))
                        Debug.LogError($"FluffyPool: Type {type} already exists and additional key is empty");
                }
                else
                {
                    InactivePoolablesByString.Add(description.Key, new HashSet<Component>(description.Count * 2));
                    var parent = new GameObject($"\"{description.Key}\"").transform;
                    parent.SetParent(_instance.transform);
                    ParentsByString.Add(description.Key, parent);

                    if (!PrefabsByString.TryAdd(description.Key, description.Prefab.Type))
                        Debug.LogError($"FluffyPool: Key {description.Key} already exists");
                }
            }
        }

        private void Awake()
        {
            Poolable.ReleaseEvent += Return;
            if (_instance != null)
            {
                Debug.LogWarning("FluffyPool: Pool is already exists. Destroying...");
                Destroy(gameObject);
            }
            _instance = this;
            Init();
            StartCoroutine(SoftInit());
        }

        private IEnumerator SoftInit()
        {
            Debug.Log("FluffyPool: Soft init started");
            foreach (var description in poolDescription)
            {
                Component newPoolable;
                if (string.IsNullOrEmpty(description.Key))
                {
                    var type = description.Prefab.Type.GetType();
                    for (int i = 0; i < description.Count; i++)
                    {
                        newPoolable = CreateNew(type);
                        newPoolable.gameObject.SetActive(false);
                        yield return null;
                    }
                }
                else
                {
                    for (int i = 0; i < description.Count; i++)
                    {
                        newPoolable = CreateNew(description.Key);
                        newPoolable.gameObject.SetActive(false);
                        yield return null;
                    }
                }
            }
            Debug.Log("FluffyPool: Soft init finished");
        }

        private void OnDestroy()
        {
            Poolable.ReleaseEvent -= Return;
        }

        [Serializable]
        private class PoolDescription
        {
            public PoolDescription(int count)
            {
                Count = count;
            }

            [field: SerializeField] public Poolable Prefab { get; private set; }
            [field: SerializeField, Min(1)] public int Count { get; private set; }
            [field: SerializeField] public string Key { get; private set; }
        }
    }
}