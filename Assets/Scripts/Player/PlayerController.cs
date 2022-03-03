using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;

    [SerializeField] private Attack basicAttack;
    [SerializeField] private Attack specialAttack;
    [SerializeField] private HitBox hitBox;

    private AttackController _attackController;
    private AudioSource _playerAudioSource;

    private PlayerControls _controls;
    private Vector2 _playerMovement = Vector2.zero;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.GamePlay.Move.performed += ctx => _playerMovement = ctx.ReadValue<Vector2>();
        _controls.GamePlay.Move.canceled += ctx => _playerMovement = Vector2.zero;
        _controls.GamePlay.Jump.performed += ctx => Jump();
        _controls.GamePlay.Attack.performed += ctx => BasicAttackClicked();
        _controls.GamePlay.SpecialAttack.performed += ctx => SpecialAttackClicked();
        // _controls.GamePlay.Dash.performed += ctx => Dash();
        // _controls.GamePlay.Dash.canceled += ctx => Roll();
        // _controls.GamePlay.Attack.performed += ctx => BasicAttack();
        // _controls.GamePlay.Pause.performed += ctx => OnPauseClick();
    }

    private void Start()
    {
        _playerAudioSource = GetComponent<AudioSource>();
        _attackController = new AttackController(bodyController.CharacterAnimationController, hitBox, _playerAudioSource);
    }

    private void OnEnable()
    {
        _controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        _controls.GamePlay.Disable();
    }

    void Update()
    {
        if (_attackController.NeedToProcessAttack())
        {
            bodyController.Freeze();
            StartCoroutine(_attackController.NextAttack());
        }
        else
        {
            bodyController.UpdateBody(_playerMovement);
        }
    }

    private void BasicAttackClicked()
    {
        _attackController.AddAttack(basicAttack);
    }
    
    private void SpecialAttackClicked()
    {
        _attackController.AddAttack(specialAttack);
    }

    private void Jump()
    {
        bodyController.Jump();
    }
}