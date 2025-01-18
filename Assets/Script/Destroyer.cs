using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
