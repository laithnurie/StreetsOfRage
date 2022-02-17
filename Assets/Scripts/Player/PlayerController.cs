using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float continuousAttackDelay = 1f;

    [SerializeField] private Attack basicAttack;
    [SerializeField] private Attack specialAttack;

    private PlayerControls _controls;
    private CharacterAnimationController _characterController;
    private Vector2 _playerMovement = Vector2.zero;

    private float _nextAttackTime = 0f;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.GamePlay.Move.performed += ctx => _playerMovement = ctx.ReadValue<Vector2>();
        _controls.GamePlay.Move.canceled += ctx => _playerMovement = Vector2.zero;
        _controls.GamePlay.Jump.performed += ctx => Jump();
        _controls.GamePlay.Attack.performed += ctx => Attack();
        // _controls.GamePlay.Dash.performed += ctx => Dash();
        // _controls.GamePlay.Dash.canceled += ctx => Roll();
        // _controls.GamePlay.Attack.performed += ctx => BasicAttack();
        // _controls.GamePlay.SpecialAttack.performed += ctx => SpecialAttack();
        // _controls.GamePlay.Pause.performed += ctx => OnPauseClick();
    }

    private void OnEnable()
    {
        _controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        _controls.GamePlay.Disable();
    }

    private void Start()
    {
        _characterController = new CharacterAnimationController(GetComponent<Animator>(), transform);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.IsMidAnim()) return;
        Move(_playerMovement);
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

    private void Jump()
    {
    }

    private void Attack()
    {
        BasicAttack();
    }

    private void BasicAttack()
    {
        // if (!_isGrounded) return;
        ProcessAttack(basicAttack, _characterController.Attack1);
    }

    private void Attack2()
    {
        // if (!_isGrounded) return;
        ProcessAttack(basicAttack, _characterController.Attack2);
    }

    private void ProcessAttack(Attack attack, string attackAnimation, Action preAttack = null)
    {
        var currentTime = Time.time;
        if (_nextAttackTime > currentTime || _characterController.IsMidAnim()) return;
        preAttack?.Invoke();
        //TODO hitbox
        // hitbox.AssignedAttack = attack;
        _nextAttackTime = currentTime + attack.Cooldown;
        StartCoroutine(_characterController.AnimateOnce(
            newAnimation: attackAnimation,
            idle: IsIdle()
        ));
    }

    private bool IsIdle() => _rigidbody2D.velocity.x == 0 && _rigidbody2D.velocity.y == 0;
}