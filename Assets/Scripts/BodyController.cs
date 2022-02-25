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
        if (_isGround)
        {
            // MoveOnGround(playerMovement);
        }
        else
        {
            MoveMidAir(playerMovement);
        }
    }

    public void Jump()
    {
        if (!_isGround) return;
        var currentVelocity = _rigidbody2D.velocity;
        currentVelocity = new Vector3(currentVelocity.x, currentVelocity.y + jump);
        _rigidbody2D.velocity = currentVelocity;
        _rigidbody2D.gravityScale = gravityScale;
        _characterController.ChangeAnimation(_characterController.Jump);
    }

    private void MoveOnGround(Vector2 playerMovement)
    {
        if (playerMovement != Vector2.zero)
        {
            gameObject.transform.localScale = new Vector3(GetDirection(playerMovement.x), 1f, 1f);
        }

        _characterController.ChangeAnimation(playerMovement.x == 0 && playerMovement.y == 0
            ? _characterController.Idle
            : _characterController.Walk);

        var newVelocity = new Vector3(playerMovement.x * speed, playerMovement.y * speed);
        _rigidbody2D.velocity = newVelocity;
    }

    private void MoveMidAir(Vector2 playerMovement)
    {
        if (_rigidbody2D.velocity == Vector2.zero) return;
        gameObject.transform.localScale = new Vector3(GetDirection(playerMovement.x), 1f, 1f);
        var jumpFallAnimation = _rigidbody2D.velocity.y < 0f ? _characterController.Fall : _characterController.Jump;
        _characterController.ChangeAnimation(jumpFallAnimation);

        var newVelocity = new Vector3(playerMovement.x * speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = newVelocity;
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
        _isGround = isGround;
    }

    public void ToggleGravity(bool enable)
    {
        _rigidbody2D.gravityScale = enable? gravityScale: 0;
    }

    public void ResetPositionRelativeToShadow()
    {
        transform.position = new Vector3(transform.position.x, groupShadow.transform.position.y + groupShadow.Offset);
    }
}