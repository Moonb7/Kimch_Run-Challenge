using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1f;
    public Vector3 startPsosition;

    public PoolKey poolKey;

    void Update()
    {
        if (transform.position.x < -15)
        {
            ObjectPoolManager.Instance.ReleaseObject(poolKey, this.gameObject);
        }

        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime;
    }
}
