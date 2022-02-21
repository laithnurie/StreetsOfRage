using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BodyController bodyController;
    [SerializeField] private GroundShadow groupShadow;

    [SerializeField] private Attack basicAttack;
    [SerializeField] private Attack specialAttack;

    private PlayerControls _controls;
    private Vector2 _playerMovement = Vector2.zero;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.GamePlay.Move.performed += ctx => _playerMovement = ctx.ReadValue<Vector2>();
        _controls.GamePlay.Move.canceled += ctx => _playerMovement = Vector2.zero;
        _controls.GamePlay.Jump.performed += ctx => Jump();
        // _controls.GamePlay.Attack.performed += ctx => Attack();
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

    // Update is called once per frame
    void Update()
    {
        bodyController.UpdateBody(_playerMovement);
    }

    private void Jump()
    {
        bodyController.Jump();
        groupShadow.Jump();
    }
}