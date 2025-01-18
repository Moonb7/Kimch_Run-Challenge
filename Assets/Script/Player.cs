using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce;
    public float invincibleTime = 5f;
    public int maxLives = 3;

    private Rigidbody2D playerRigidbody;
    private Animator palyerAnimator;
    private BoxCollider2D playerCollider;


    [Header("References")]
    [SerializeField]
    private bool isGrounded = true;

    [SerializeField]
    private bool isInvincible = false;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        palyerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        playerRigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        palyerAnimator.SetInteger("state", 1);
    }

    void Hit()
    {
        GameManager.Instance.lives -= 1;
    }
    void Heal()
    {
        GameManager.Instance.lives = Math.Min(maxLives, GameManager.Instance.lives + 1);
    }
    void StartInvincible()
    {
        CancelInvoke("StopInvincible");
        isInvincible = true;
        Invoke("StopInvincible", invincibleTime);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    public void KillPlayer()
    {
        playerCollider.enabled = false;
        palyerAnimator.enabled = false;
        playerRigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (!isGrounded)
            {
                palyerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        switch (collider.gameObject.tag)
        {
            case "Enemy":
                if (!isInvincible)
                {
                    Destroy(collider.gameObject);
                    Hit();
                }
                break;
            case "Food":
                Destroy(collider.gameObject);
                Heal();
                break;
            case "Golden":
                Destroy(collider.gameObject);
                StartInvincible();
                break;
        }
    }


}
