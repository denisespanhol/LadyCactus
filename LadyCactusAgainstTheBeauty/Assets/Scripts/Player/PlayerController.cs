using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    [Header("Character Speed")]
    [SerializeField] private float speedWalk;

    private Animator _playerAnimator;
    private PlayerHealth _playerHealth;
    private Rigidbody2D _playerRigidbody2D;
    private Vector2 moveInput;

    #endregion

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!_playerHealth.isDeath) HandleMovement();
    }

    // function to control the players movement
    private void HandleMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        _playerRigidbody2D.velocity = moveInput * speedWalk;

        PlayerAnimation();
    }

    private void PlayerAnimation()
    {
        bool isMovingDown = moveInput.y < 0;
        bool isMovingUp = moveInput.y > 0;
        bool isMovingLeft = moveInput.x < 0;
        bool isMovingRight = moveInput.x > 0;

        Vector3 rotation = transform.rotation.eulerAngles;

        if (isMovingDown)
        {
            _playerAnimator.SetBool("isFrontWalking", true);
            _playerAnimator.SetBool("isBackWalking", false);
            _playerAnimator.SetBool("isSideWalking", false);
        }
        else if (isMovingUp)
        {
            _playerAnimator.SetBool("isFrontWalking", false);
            _playerAnimator.SetBool("isBackWalking", true);
            _playerAnimator.SetBool("isSideWalking", false);

        }
        else if (isMovingLeft)
        {
            _playerAnimator.SetBool("isFrontWalking", false);
            _playerAnimator.SetBool("isBackWalking", false);
            _playerAnimator.SetBool("isSideWalking", true);

            rotation = new Vector3(rotation.x, -180, rotation.z);
            transform.rotation = Quaternion.Euler(rotation);
        }
        else if (isMovingRight)
        {
            _playerAnimator.SetBool("isFrontWalking", false);
            _playerAnimator.SetBool("isBackWalking", false);
            _playerAnimator.SetBool("isSideWalking", true);

            rotation = new Vector3(rotation.x, 0, rotation.z);
            transform.rotation = Quaternion.Euler(rotation);
        }
        else
        {
            _playerAnimator.SetBool("isFrontWalking", false);
            _playerAnimator.SetBool("isBackWalking", false);
            _playerAnimator.SetBool("isSideWalking", false);
        }
    }

}
