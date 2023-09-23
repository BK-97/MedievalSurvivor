using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class MultiGameObjectPool : Singleton<MultiGameObjectPool>
{
    private Dictionary<GameObject, ObjectPool<GameObject>> objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
    private Dictionary<string, GameObject> prefabsDict = new Dictionary<string, GameObject>();

    [SerializeField]
    private List<GameObject> prefabs;

    [SerializeField]
    private int initialSize = 10;

    [SerializeField]
    private int maxSize = 100;

    private void Awake()
    {
        foreach (GameObject prefab in prefabs)
        {
            prefabsDict[prefab.name] = prefab;
            ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(() => CreatePooledObject(prefab), OnTakeFromPool, OnReturnToPool, OnDestroyObject, true, initialSize, maxSize);
            objectPools[prefab] = objectPool;
        }
    }

    private GameObject CreatePooledObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.SetActive(false);
        return obj;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
    public GameObject GetObject(string prefabName) //USE THIS METHOD
    {
        GameObject prefab;
        if (prefabsDict.TryGetValue(prefabName, out prefab))
        {
            ObjectPool<GameObject> objectPool;
            if (objectPools.TryGetValue(prefab, out objectPool))
            {
                return objectPool.Get();
            }
            else
            {
                Debug.LogWarning("The requested prefab is not registered in the object pools.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("The requested prefab is not registered in the MultiGameObjectPool.");
            return null;
        }
    }
    public GameObject GetObject(string prefabName,Vector3 pos) //USE THIS METHOD
    {
        GameObject prefab;
        if (prefabsDict.TryGetValue(prefabName, out prefab))
        {
            ObjectPool<GameObject> objectPool;
            if (objectPools.TryGetValue(prefab, out objectPool))
            {
                return objectPool.Get();
            }
            else
            {
                Debug.LogWarning("The requested prefab is not registered in the object pools.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("The requested prefab is not registered in the MultiGameObjectPool.");
            return null;
        }
    }
    public GameObject GetObject(GameObject prefab)
    {
        ObjectPool<GameObject> objectPool;
        if (objectPools.TryGetValue(prefab, out objectPool))
        {
            return objectPool.Get();
        }
        else
        {
            Debug.LogWarning("The requested prefab is not registered in the object pools.");
            return null;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        GameObject prefab = obj.GetComponent<GameObject>();
        if (prefab != null)
        {
            ObjectPool<GameObject> objectPool;
            if (objectPools.TryGetValue(prefab, out objectPool))
            {
                objectPool.Release(obj);
            }
            else
            {
                Debug.LogWarning("The prefab of the returned object is not registered in the object pools.");
            }
        }
        else
        {
            Debug.LogWarning("The returned object is not a GameObject.");
        }
    }
}