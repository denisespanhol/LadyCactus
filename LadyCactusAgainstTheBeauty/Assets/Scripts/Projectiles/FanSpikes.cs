using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpikes : MonoBehaviour
{
    [Header ("Spike Objects")]
    [SerializeField] private GameObject spike;
    [SerializeField] private Animator _playerAnimator;

    [Header("Spike Positions")]
    [SerializeField] private float upPosition;
    [SerializeField] private float downPosition;
    [SerializeField] private float rightPosition;
    [SerializeField] private float leftPosition;

    private float[] spikeAnglesUp = { 20f, 10f, 0f, -10f, -20f };
    private float[] spikeAnglesDown = { 200f, 190f, 180f, 170f, 160f };
    private float[] spikeAnglesRight = { -110f, -100f, -90f, -80f, -70f };
    private float[] spikeAnglesLeft = { 110f, 100f, 90f, 80f, 70f };

    private Transform playerTransform;
    private PlayerHealth _playerHealth;
    private AudioManager _audioManager;
    private float upY;
    private float downY;
    private float rightX;
    private float leftX;

    private void Awake()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!_playerHealth.isDeath) shootSpikes();
    }

    private void shootSpikes()
    {
        upY = playerTransform.position.y + upPosition;
        downY = playerTransform.position.y - downPosition;
        rightX = playerTransform.position.x + rightPosition;
        leftX = playerTransform.position.x - leftPosition;

        Vector3 rotation = playerTransform.rotation.eulerAngles;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _audioManager.PlaySFX(_audioManager.cactusAttack);
            transform.position = new Vector3(playerTransform.position.x, upY, playerTransform.position.z);
            for (int index = 0; index < spikeAnglesUp.Length; index += 1)
            {
                Instantiate(spike, transform.position, Quaternion.Euler(0, 0, spikeAnglesUp[index]));
            }
            _playerAnimator.SetTrigger("BackAttack");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _audioManager.PlaySFX(_audioManager.cactusAttack);
            transform.position = new Vector3(playerTransform.position.x, downY, playerTransform.position.z);
            for (int index = 0; index < spikeAnglesDown.Length; index += 1)
            {
                Instantiate(spike, transform.position, Quaternion.Euler(0, 0, spikeAnglesDown[index]));
            }
            _playerAnimator.SetTrigger("FrontAttack");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _audioManager.PlaySFX(_audioManager.cactusAttack);
            transform.position = new Vector3(rightX, playerTransform.position.y, playerTransform.position.z);
            for (int index = 0; index < spikeAnglesRight.Length; index += 1)
            {
                Instantiate(spike, transform.position, Quaternion.Euler(0, 0, spikeAnglesRight[index]));
            }
            rotation = new Vector3(rotation.x, 0, rotation.z);
            playerTransform.rotation = Quaternion.Euler(rotation);
            _playerAnimator.SetTrigger("isAttacking");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _audioManager.PlaySFX(_audioManager.cactusAttack);
            transform.position = new Vector3(leftX, playerTransform.position.y, playerTransform.position.z);
            for (int index = 0; index < spikeAnglesLeft.Length; index += 1)
            {
                Instantiate(spike, transform.position, Quaternion.Euler(0, 0, spikeAnglesLeft[index]));
            }
            rotation = new Vector3(rotation.x, -180, rotation.z);
            playerTransform.rotation = Quaternion.Euler(rotation);
            _playerAnimator.SetTrigger("isAttacking");
        }
    }
}
