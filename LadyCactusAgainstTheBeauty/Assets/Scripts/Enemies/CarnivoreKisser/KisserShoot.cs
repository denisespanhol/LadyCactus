using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KisserShoot : MonoBehaviour
{
    public float bulletLife;
    public float bulletRotation;
    public float bulletSpeed;

    private Vector2 _spawnPoint;
    private float _timer = 0f;

    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        ToMovement();
        CheckLifetime();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private Vector2 Movement(float timer)
    {
        // Moves right according to the bulet's rotation
        float x = _timer * bulletSpeed * transform.right.x;
        float y = _timer * bulletSpeed * transform.right.y;

        return new Vector2(x + _spawnPoint.x, y + _spawnPoint.y);
    }

    private void ToMovement()
    {
        _timer += Time.deltaTime;

        transform.position = Movement(_timer);
    }

    private void CheckLifetime()
    {
        // Check the bullet's lifetime
        if (_timer > bulletLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerHealth.isTakingDamage)
        {
            if (collision.CompareTag("Player")) Destroy(gameObject);
        }
    }
}
