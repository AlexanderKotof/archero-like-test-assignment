using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Newbeedev.ObjectsPool
{
    public class ObjectSpawnManager : Singletone<ObjectSpawnManager>, IDisposable
    {
        private readonly Dictionary<Component, ObjectPool<Component>> _componentToObjectPools = new Dictionary<Component, ObjectPool<Component>>();

        private Transform _parent;

        public void Initialize()
        {
            _parent = new GameObject("ObjectsPool").transform;
        }

        public void Dispose()
        {
            foreach (var pool in _componentToObjectPools.Values)
            {
                pool.Dispose();
            }
            _componentToObjectPools.Clear();
        }

        private ObjectPool<Component> RegisterPrefab(Component prefab)
        {
            if (_componentToObjectPools.TryGetValue(prefab, out var pool))
            {
                return pool;
            }

            Debug.Log($"Registered new prefab {prefab.gameObject.name} of type {prefab.GetType().Name}");

            pool = new ObjectPool<Component>(prefab, _parent);
            _componentToObjectPools.Add(prefab, pool);

            return pool;
        }

        public static Component Spawn(Component prefab)
        {
            var pool = Instance.RegisterPrefab(prefab);
            return pool.Pool();
        }

        public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            var spawned = Spawn(prefab);
            spawned.transform.position = position;
            spawned.transform.rotation = rotation;
            return spawned;
        }

        public static T Spawn<T>(T component) where T : Component
        {
            return Spawn((Component)component) as T;
        }

        public static void Despawn(Component instance)
        {
            instance.gameObject.SetActive(false);
        }

        public static void DespawnAll()
        {
            foreach (var pool in Instance._componentToObjectPools.Values)
            {
                pool.DespawnAll();
            }
        }

        public async static void DespawnAfter(Component instance, float timeSec)
        {
            await Task.Delay((int)(timeSec * 1000));
            instance.gameObject.SetActive(false);
        }
    }
}
