using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    [Header("References")]
    public PoolKey poolKey;
    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        GameObject pooledObject = ObjectPoolManager.Instance.GetObject(poolKey);
        if (pooledObject != null)
        {
            pooledObject.transform.SetParent(this.transform);
            pooledObject.transform.position = this.transform.position;
            pooledObject.transform.rotation = Quaternion.identity;
            pooledObject.SetActive(true);
        }
        else
        {
            Debug.Log($"Spawn : pooledObject이 존재하지 않습니다.");
        }

        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
