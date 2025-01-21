using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public struct PoolData
{
    public PoolKey key;
    public List<GameObject> prefabs;
    public int defaultCapacity;
    public int maxSize;
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{

    // 다양한 오브젝트 풀을 관리하기 위한 딕셔너리
    private Dictionary<PoolKey, ObjectPool<GameObject>> pools = new Dictionary<PoolKey, ObjectPool<GameObject>>();
    private Dictionary<PoolKey, int> prefabIndices = new Dictionary<PoolKey, int>();

    [Header("References")]
    public PoolData[] poolDatas;

    void Start()
    {
        foreach (var poolData in poolDatas)
        {
            CreatePool(poolData.key, poolData.prefabs, poolData.defaultCapacity, poolData.maxSize);
        }
    }

    public void CreatePool(PoolKey key, List<GameObject> prefabs, int defaultCapacity = 6, int maxSize = 20)
    {
        if (pools.ContainsKey(key))
        {
            Debug.Log($"CreatePool : 이미 Pool에 등록된 {key} 입니다.");
            return;
        }

        pools[key] = new ObjectPool<GameObject>(
            () => InstantiatePrefabs(key, prefabs),
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            obj => Destroy(obj),
            false,
            defaultCapacity,
            maxSize
        );
    }

    public GameObject InstantiatePrefabs(PoolKey key, List<GameObject> prefabs)
    {
        // 인덱스 값이 없으면 0으로 초기화
        if (!prefabIndices.ContainsKey(key))
        {
            prefabIndices[key] = 0;
        }

        int index = prefabIndices[key];

        if (index < prefabs.Count)
        {
            // 처음에는 순차적으로 pool할 프리팹 생성
            prefabIndices[key]++;
            return Instantiate(prefabs[index]);
        }
        else
        {
            // 이후 생성부터는 랜덤으로 생성
            return Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
        }
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
