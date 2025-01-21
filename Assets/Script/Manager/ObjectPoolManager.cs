using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    // 다양한 오브젝트 풀을 관리하기 위한 딕셔너리
    private Dictionary<PoolKey, ObjectPool<GameObject>> pools;

    public void CreatePool(PoolKey key, GameObject prefab, int defaultCapacity = 6, int maxSize = 20)
    {
        if (pools.ContainsKey(key))
        {
            Debug.Log($"CreatePool : 이미 Pool에 등록된 {key} 입니다.");
            return;
        }

        pools[key] = new ObjectPool<GameObject>(
            () => Instantiate(prefab),
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            obj => Destroy(obj),
            false,
            defaultCapacity,
            maxSize
        );
    }

    public GameObject GetObject(PoolKey key)
    {
        if (pools.ContainsKey(key))
        {
            return pools[key].Get();
        }
        Debug.Log($"GetObject : pools에 포함되어 있지 않는 {key} 입니다.");
        return null;
    }

    public void ReleaseObject(PoolKey key, GameObject obj)
    {
        if (pools.ContainsKey(key))
        {
            pools[key].Release(obj);
        }
        else
        {
            Debug.Log($"ReleaseObject : pools에 포함되어 있지 않는 {key} 입니다.");
        }
    }
}
