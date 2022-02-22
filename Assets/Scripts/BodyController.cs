using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    private CharacterAnimationController _characterController;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 5f;
    
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
        if (!_isGround && _rigidbody2D.velocity.y > 0)
        {
            _characterController.ChangeAnimation(_characterController.Fall);
        }
        Move(playerMovement);
    }

    public void Jump()
    {
        if (!_isGround) return;
        var currentVelocity = _rigidbody2D.velocity;
        currentVelocity = new Vector3(currentVelocity.x, currentVelocity.y + jump);
        _rigidbody2D.gravityScale = 10;
        _rigidbody2D.velocity = currentVelocity;
        _characterController.ChangeAnimation(_characterController.Jump);
    }

    private void Move(Vector2 playerMovement)
    {
        if (playerMovement != Vector2.zero)
        {
            gameObject.transform.localScale = new Vector3(GetDirection(playerMovement.x), 1f, 1f);
        }

        var newVelocity = new Vector3(playerMovement.x * speed, playerMovement.y * speed);
        _rigidbody2D.velocity = newVelocity;

        _characterController.ChangeAnimation(newVelocity.x == 0 && newVelocity.y == 0
            ? _characterController.Idle
            : _characterController.Walk);
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
        if (isGround) _rigidbody2D.gravityScale = 0;
    }
}
