using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    #region VARIABLES
    [Header("Bullet Options")]
    [SerializeField] private float spikeSpeed;
    [SerializeField] private float limitRangeRight;
    [SerializeField] private float limitRangeLeft;
    [SerializeField] private float limitRangeUp;
    [SerializeField] private float limitRangeDown;

    private GameObject playerGameObject;
    #endregion

    private void Start()
    {
        playerGameObject = GameObject.Find("Player");

        limitRangeRight += playerGameObject.transform.position.x;
        limitRangeLeft += playerGameObject.transform.position.x;
        limitRangeUp += playerGameObject.transform.position.y;
        limitRangeDown += playerGameObject.transform.position.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * spikeSpeed * Time.deltaTime);
        LimitSpikeRange();
    }

    private void LimitSpikeRange()
    {
        Vector3 playerPosition = playerGameObject.transform.position;

        if (transform.position.x > limitRangeRight || transform.position.x < limitRangeLeft) Destroy(gameObject);
        if (transform.position.y > limitRangeUp || transform.position.y < limitRangeDown) Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
