using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletLife;
    [SerializeField] private float _bulletSpeed;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType _spawnerType;
    [SerializeField] private float _firingRate;

    [Header("Interval Attributes")]
    [SerializeField] private float interval = 2f;
    [SerializeField] private float shootingDuration = 3f;

    [Header("Enemy Animator")]
    [SerializeField] private Animator _kisserAnimator;

    private GameObject _spawnedBullet;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private bool _isSoundEffectPlayed = false;
    private float _timer = 0f;

    [HideInInspector] public bool _isFashiowDeath = false;
    [HideInInspector] public bool _isCarnivoreDeath = false;

    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(IntervalToShoot());
    }

    private void Update()
    {
        if (_isFashiowDeath)
        {
            StopCoroutine(IntervalToShoot());

            if (!_isSoundEffectPlayed)
            {
                if (_enemy.CompareTag("FashiowPlant")) _audioManager.PlaySFX(_audioManager.fashiowPlantDeath);
                if (_enemy.CompareTag("CarnivoreKisser")) _audioManager.PlaySFX(_audioManager.carnivoreKisserDeath);
                _isSoundEffectPlayed = true;
            }

            _kisserAnimator.SetTrigger("Death");
            FashiowDeath();
        }
    }

    private void Fire()
    {
        if (_bullet && _enemy.CompareTag("CarnivoreKisser"))
        {
            _spawnedBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            _spawnedBullet.GetComponent<KisserShoot>().bulletSpeed = _bulletSpeed;
            _spawnedBullet.GetComponent<KisserShoot>().bulletLife = _bulletLife;
            _spawnedBullet.transform.rotation = transform.rotation;
        }

        if (_bullet && _enemy.CompareTag("FashiowPlant"))
        {
            _spawnedBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            _spawnedBullet.GetComponent<FashiowPlant>().bulletSpeed = _bulletSpeed;
            _spawnedBullet.GetComponent<FashiowPlant>().bulletLife = _bulletLife;
            _spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    private void FiredBullet()
    {
        _timer += Time.deltaTime;

        if (_spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

        if (_timer >= _firingRate)
        {
            Fire();
            if (_enemy.CompareTag("FashiowPlant")) _kisserAnimator.SetTrigger("Attack");
            _timer = 0f;
        }
    }

    IEnumerator IntervalToShoot()
    {
        while (true)
        {
            // Atira por shootingDuration segundos
            float shootingElapsedTime = 0f;

            while (shootingElapsedTime < shootingDuration)
            {
                if (_enemy.CompareTag("CarnivoreKisser")) _kisserAnimator.SetBool("isAttacking", true);
                FiredBullet();
                shootingElapsedTime += Time.deltaTime;
                yield return null;
            }

            if (_enemy.CompareTag("CarnivoreKisser")) _kisserAnimator.SetBool("isAttacking", false);
            // Espera pelo intervalo especificado
            yield return new WaitForSeconds(interval);
        }
    }

    private void FashiowDeath()
    {
        StartCoroutine(WaitToDeath());
    }

    private IEnumerator WaitToDeath()
    {
        float interval = 0.8f;

        yield return new WaitForSeconds(interval);

        _isSoundEffectPlayed = false;
        if (_enemy.CompareTag("FashiowPlant")) _gameManager.ToMakePoints(50);
        if (_enemy.CompareTag("CarnivoreKisser")) _gameManager.ToMakePoints(100);
        Destroy(_enemy.gameObject);
    }
}

