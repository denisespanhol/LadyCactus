using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed;

    private GameManager _gameManager;
    private AudioManager _audioManager;
    private Animator _flowerAnimator;

    private Transform _playerTransform;
    private PlayerHealth _playerHealth;
    private bool _isSoundEffectPlayed = false;

    [HideInInspector] public bool _isFlowerDeath = false;
    

    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _flowerAnimator = GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (!_playerHealth.isDeath)
        {
            if (!_isFlowerDeath)
                transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _enemySpeed * Time.deltaTime);
            else
            {
                if (!_isSoundEffectPlayed)
                {
                    _audioManager.PlaySFX(_audioManager.littleFlowerDeath);
                    _isSoundEffectPlayed = true;
                }
                _flowerAnimator.SetTrigger("Death");
                FlowerDeath();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerHealth.isTakingDamage)
        {
            if (collision.CompareTag("Player")) _isFlowerDeath = true;
        }
    }

    private void FlowerDeath()
    {
        StartCoroutine(WaitToDeath());
    }

    private IEnumerator WaitToDeath()
    {
        float interval = 0.8f;

        yield return new WaitForSeconds(interval);

        _gameManager.ToMakePoints(25);
        _isSoundEffectPlayed = false;
        Destroy(gameObject);
    }
}
