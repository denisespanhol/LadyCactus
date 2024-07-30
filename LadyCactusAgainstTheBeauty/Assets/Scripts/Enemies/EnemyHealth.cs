using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private BasicEnemy _flowerEnemy;
    private ShootSpawner _spawner;


    private void Start()
    {
        if (CompareTag("enemy")) _flowerEnemy = GetComponent<BasicEnemy>();
        if (CompareTag("FashiowPlant") || CompareTag("CarnivoreKisser")) _spawner = GetComponentInChildren<ShootSpawner>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spike"))
        {
            if (_flowerEnemy != null)
            {
                _flowerEnemy._isFlowerDeath = true;
                Destroy(collision.gameObject);
            }

            if (_spawner != null && CompareTag("FashiowPlant"))
            {
                _spawner._isFashiowDeath = true;
                Destroy(collision.gameObject);
            }

            if (_spawner != null && CompareTag("CarnivoreKisser"))
            {
                _spawner._isFashiowDeath = true;
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Player"))
        {
            if (_spawner != null && (CompareTag("FashiowPlant") || CompareTag("CarnivoreKisser")))
            {
                _spawner._isFashiowDeath = true;
            }
        }
    }
}
