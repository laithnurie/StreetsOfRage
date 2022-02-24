using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private GroundShadow groupShadow;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private float gravityScale = 10f;

    private CharacterAnimationController _characterController;
    private Rigidbody2D _rigidbody2D;
    private bool _isGround = true;

    void Start()
    {
        _characterController = new CharacterAnimationController(GetComponent<Animator>(), transform);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void UpdateBody(Vector2 playerMovement)
    {
        if (_characterController.IsMidAnim()) return;
        if (!_isGround)
        {
            if (_rigidbody2D.velocity != Vector2.zero)
            {
                var jumpFallAnimation =
                    _rigidbody2D.velocity.y < 0f ? _characterController.Fall : _characterController.Jump;
                _characterController.ChangeAnimation(jumpFallAnimation);
            }
        }

        // Move(playerMovement);
    }

    public void Jump()
    {
        if (!_isGround) return;
        var currentVelocity = _rigidbody2D.velocity;
        currentVelocity = new Vector3(currentVelocity.x, currentVelocity.y + jump);
        _rigidbody2D.velocity = currentVelocity;
        _rigidbody2D.gravityScale = gravityScale;
        groupShadow.Jump();
        _characterController.ChangeAnimation(_characterController.Jump);
    }

    private void Move(Vector2 playerMovement)
    {
        if (playerMovement != Vector2.zero)
        {
            gameObject.transform.localScale = new Vector3(GetDirection(playerMovement.x), 1f, 1f);
        }

        var currentPosition = transform.position;
        var yPosition = _isGround ? currentPosition.y + playerMovement.y : transform.position.y;
        currentPosition = new Vector3(
            currentPosition.x + playerMovement.x,
            yPosition
        );

        transform.position = currentPosition;

        groupShadow.MoveShadow(currentPosition);

        if (_isGround)
        {
            _characterController.ChangeAnimation(playerMovement.x == 0 && playerMovement.y == 0
                ? _characterController.Idle
                : _characterController.Walk);
        }
    }

    private float GetDirection(float xVelocity)
    {
        // check negative or positive to switch the character around
        if (xVelocity == 0) return gameObject.transform.localScale.x;
        if (xVelocity < 0) return -1f;
        return 1f;
    }

    public void UpdateIsGround(bool isGround)
    {
        StartCoroutine(IsGroundDelay(isGround));
    }

    public void DisableGravity()
    {
        _rigidbody2D.gravityScale = 0;
    }

    private IEnumerator IsGroundDelay(bool isGround)
    {
        yield return new WaitForSeconds(0f);
        _isGround = isGround;
    }
}