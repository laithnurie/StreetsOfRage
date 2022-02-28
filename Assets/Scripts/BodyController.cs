using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private GroundShadow groundShadow;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 30f;
    [SerializeField] private float gravityScale = 10f;
    [SerializeField] private float jumpPadding = 0.5f;

    //TODO: singleton pattern setup later
    [SerializeField] private LevelController levelController;

    private bool midJump = false;

    private CharacterAnimationController _characterController;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private float _colliderWidth;

    void Start()
    {
        _characterController = new CharacterAnimationController(GetComponent<Animator>(), transform);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _colliderWidth = _collider2D.bounds.size.x / 2;
    }

    public void UpdateBody(Vector2 playerMovement)
    {
        if (_characterController.IsMidAnim()) return;

        var currentDistance = Vector3.Distance(transform.position, groundShadow.transform.position);
        var inAir = currentDistance >= jumpPadding && midJump;

        if (inAir)
        {
            _rigidbody2D.gravityScale = gravityScale;
            _collider2D.isTrigger = true;
            MoveMidAir(playerMovement);
        }
        else
        {
            _rigidbody2D.gravityScale = 0;
            _collider2D.isTrigger = false;
            MoveOnGround(playerMovement);
        }
    }

    public void Jump()
    {
        if (midJump) return;
        groundShadow.LockYAxis(true);
        midJump = true;
        var currentVelocity = _rigidbody2D.velocity;
        currentVelocity = new Vector3(currentVelocity.x, currentVelocity.y + jump);
        _rigidbody2D.velocity = currentVelocity;
        _rigidbody2D.gravityScale = gravityScale;
        _characterController.ChangeAnimation(_characterController.Jump);

        StartCoroutine(WaitForJump());
    }

    private IEnumerator WaitForJump()
    {
        yield return new WaitUntil(() => _rigidbody2D.velocity.y == 0);
        groundShadow.LockYAxis(false);
        midJump = false;
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

        if (!levelController.PlayerIsInXBounds(transform.position.x, _colliderWidth))
        {
            Debug.Log("Player reached Bounds");
            ReachXBounds();
        }
    }

    private float GetDirection(float xVelocity)
    {
        // check negative or positive to switch the character around
        if (xVelocity == 0) return gameObject.transform.localScale.x;
        if (xVelocity < 0) return -1f;
        return 1f;
    }

    public void ResetPositionRelativeToShadow(float offset)
    {
        transform.position = new Vector3(transform.position.x, groundShadow.transform.position.y + offset);
        _collider2D.isTrigger = false;
    }

    public CharacterAnimationController CharacterAnimationController => _characterController;

    public void Freeze()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void ReachXBounds()
    {
        _rigidbody2D.velocity = new Vector3(0, _rigidbody2D.velocity.y);
    }
}