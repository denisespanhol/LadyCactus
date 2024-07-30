using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Animator _tryAgainAnimator;

    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;

    [HideInInspector] public bool isTakingDamage = true;
    [HideInInspector] public bool isDeath = false;

    public int currentHealth;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();
        currentHealth = _maxHealth;
    }


    public void TakeDamage()
    {
        currentHealth -= 1;

        if (currentHealth <= 0 )
        {
            isDeath = true;
        }
        playerDeath();
    }

    private void playerDeath()
    {
        if(isDeath)
        {
            _playerAnimator.SetTrigger("Death");
            StartCoroutine(ToShowTryAgain());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDeath)
        {
            if (collision.CompareTag("enemyBullet") || collision.CompareTag("enemy") || collision.CompareTag("FashiowPlant") || collision.CompareTag("CarnivoreKisser"))
            {
                if (isTakingDamage)
                {
                    TakeDamage();
                    StopDamageAnimation();
                    Debug.Log(currentHealth);
                }
            }
        }
    }

    private void StopDamageAnimation()
    {
        isTakingDamage = false;
        _playerAnimator.SetTrigger("Damaged");
        StartCoroutine(FlashAfterDamage());
    }

    private IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.0833f;

        for (int index = 0; index < 10; index += 1)
        {
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDelay);
        }
        isTakingDamage = true;
    }

    private IEnumerator ToShowTryAgain()
    {
        yield return new WaitForSeconds(2f);

        _tryAgainAnimator.SetTrigger("GameOver");
    }
}
