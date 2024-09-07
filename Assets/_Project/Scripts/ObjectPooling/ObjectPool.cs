using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public Transform Container { get; private set; }

    private List<T> _pool;
    private List<T> _prefabs;

    public ObjectPool(T prefab, int prefabsCount, Transform container = null)
    {
        List<T> prefabs = new(1) { prefab };

        Initialize(prefabs, prefabsCount, container);
    }

    public ObjectPool(List<T> prefabs, int prefabsCount, Transform container = null)
    {
        Initialize(prefabs, prefabsCount, container);
    }

    public bool HasFreeObject(out T element)
    {
        var attemptsNumber = 50;

        for (int i = 0; i < attemptsNumber; i++)
        {
            var item = GetRandomPrefabFromPool();

            if (!item.isActiveAndEnabled)
            {
                element = item;
                return true;
            }
        }

        foreach (var item in _pool)
        {
            if (!item.isActiveAndEnabled)
            {
                element = item;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetObject(Vector3 spawnPosition)
    {
        if (HasFreeObject(out var element))
        {
            element.transform.position = spawnPosition;
            element.gameObject.SetActive(true);
            return element;
        }

        T currentObject = CreateObject(true);
        currentObject.transform.position = spawnPosition;
        return currentObject;
    }

    private void CreatePool(int prefabsCount)
    {
        _pool = new List<T>();

        for (int i = 0; i < _prefabs.Count; i++)
        {
            for (int j = 0; j < prefabsCount; j++)
                CreateObject();
        }
    }

    private T CreateObject(bool isActive = false)
    {
        var createdObject = Object.Instantiate(GetRandomPrefabFromPrefabs(), Container);
        createdObject.gameObject.SetActive(isActive);
        _pool.Add(createdObject);
        return createdObject;
    }

    private T GetRandomPrefabFromPrefabs()
    {
        var randomNumber = Random.Range(0, _prefabs.Count - 1);

        return _prefabs[randomNumber];
    }

    private T GetRandomPrefabFromPool()
    {
        var randomNumber = Random.Range(0, _pool.Count - 1);

        return _pool[randomNumber];
    }

    private void Initialize(List<T> prefabs, int prefabsCount, Transform container = null)
    {
        _prefabs = prefabs;
        Container = container;

        CreatePool(prefabsCount);
    }
}