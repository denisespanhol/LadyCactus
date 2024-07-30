using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region VARIABLES

    [Header("Player Game Object")]
    [SerializeField] private GameObject playerObj;

    [Header("Limit Position to player doesn't leave screen")]
    [SerializeField] private float screenLimitX;
    [SerializeField] private float screenLimitY;

    [Header("Pontuation")]
    [SerializeField] private TextMeshProUGUI _pontuation;
    [SerializeField] private TextMeshProUGUI _record;
    [SerializeField] private TextMeshProUGUI _finalScore;

    [Header("Life Points")]
    [SerializeField] private Image lifePoint1;
    [SerializeField] private Image lifePoint2;
    [SerializeField] private Image lifePoint3;
    [SerializeField] private Image lifePoint4;
    [SerializeField] private Image lifePoint5;

    private PlayerHealth _playerHealth;
    private Transform _playerTransform;
    private int _actualPoints = 0;
    private int _actualRecord = 0;

    #endregion

    private void Awake()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _playerTransform = playerObj.GetComponent<Transform>();
        _pontuation.text = "Score: " + _actualPoints;
        _record.text = "Record: " + PlayerPrefs.GetInt("Recorde", 0);
    }

    void Update()
    {
        PreventPlayerLeaveScreen();
        ToMakeRecord();
        LostLife();
    }

    private void PreventPlayerLeaveScreen()
    {

        if (_playerTransform.position.x <= -screenLimitX || _playerTransform.position.x >= screenLimitX)
        {
            float XClamp = Mathf.Clamp(_playerTransform.position.x, -screenLimitX, screenLimitX);

            _playerTransform.position = new Vector3(XClamp, _playerTransform.position.y, _playerTransform.position.z);
        }

        if (_playerTransform.position.y <= -screenLimitY || _playerTransform.position.y >= screenLimitY)
        {
            float YClamp = Mathf.Clamp(_playerTransform.position.y, -screenLimitY, screenLimitY);

            _playerTransform.position = new Vector3(_playerTransform.position.x, YClamp, _playerTransform.position.z);
        }
    }

    public void ToMakePoints(int points)
    {
        _actualPoints += points;

        _pontuation.text = "Score: " + _actualPoints;
    }

    private void ToMakeRecord()
    {
        if (_playerHealth.currentHealth <= 0 && PlayerPrefs.GetInt("Recorde", 0) < _actualPoints)
        {
            PlayerPrefs.SetInt("Recorde", _actualPoints);
            _record.text = "Recorde: " + _actualPoints;
        }
    }

    private void LostLife()
    {
        if (_playerHealth.currentHealth == 5)
        {
            lifePoint1.enabled = true;
            lifePoint2.enabled = true;
            lifePoint3.enabled = true;
            lifePoint4.enabled = true;
            lifePoint5.enabled = true;
        }

        if (_playerHealth.currentHealth == 4)
        {
            lifePoint1.enabled = false;
            lifePoint2.enabled = true;
            lifePoint3.enabled = true;
            lifePoint4.enabled = true;
            lifePoint5.enabled = true;
        }

        if (_playerHealth.currentHealth == 3)
        {
            lifePoint1.enabled = false;
            lifePoint2.enabled = false;
            lifePoint3.enabled = true;
            lifePoint4.enabled = true;
            lifePoint5.enabled = true;
        }

        if (_playerHealth.currentHealth == 2)
        {
            lifePoint1.enabled = false;
            lifePoint2.enabled = false;
            lifePoint3.enabled = false;
            lifePoint4.enabled = true;
            lifePoint5.enabled = true;
        }

        if (_playerHealth.currentHealth == 1)
        {
            lifePoint1.enabled = false;
            lifePoint2.enabled = false;
            lifePoint3.enabled = false;
            lifePoint4.enabled = false;
            lifePoint5.enabled = true;
        }

        if (_playerHealth.currentHealth <= 0)
        {
            lifePoint1.enabled = false;
            lifePoint2.enabled = false;
            lifePoint3.enabled = false;
            lifePoint4.enabled = false;
            lifePoint5.enabled = false;

            _finalScore.text = "Score: " + _actualPoints;
        }
    }
}
