using UnityEngine;
using UnityEngine.Android;

public class Heart : MonoBehaviour
{
    [Header("References")]
    public Sprite onHeart;
    public Sprite offHeart;
    public int liveNumber;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.lives >= liveNumber)
        {
            spriteRenderer.sprite = onHeart;
        }
        else
        {
            spriteRenderer.sprite = offHeart;
        }
    }
}
