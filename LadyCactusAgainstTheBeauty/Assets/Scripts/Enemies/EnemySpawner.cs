using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spanw Object and Time")]
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private float _minimumSpawnTime;
    [SerializeField] private float _maximumSpawnTime;

    [Header("Spawn Location Limits")]
    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minX;

    private float _timeUntilSpawn;
    private float _yPosition;
    private float _xPosition;


    void Awake()
    {
        SetTimeUntilSpawn();
        _yPosition = Random.Range(_minY, _maxY);
        _xPosition = Random.Range(_minX, _maxX);
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0 )
        {
            if (CompareTag("FashiowPlant"))
            {
                if (_xPosition >= 0) 
                    Instantiate(_Enemy, new Vector3(_xPosition, _yPosition, transform.position.z), Quaternion.Euler(0f, 180f, 0f));
                else 
                    Instantiate(_Enemy, new Vector3(_xPosition, _yPosition, transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(_Enemy, new Vector3(_xPosition, _yPosition, transform.position.z), Quaternion.identity);   
            }
            SetTimeUntilSpawn();
            _yPosition = Random.Range(_minY, _maxY);
            _xPosition = Random.Range(_minX, _maxX);
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
