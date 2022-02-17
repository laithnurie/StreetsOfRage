using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed;
    
    private PlayerControls _controls;
    private CharacterAnimationController _characterController;
    private Vector2 _playerMovement = Vector2.zero;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.GamePlay.Move.performed += ctx => _playerMovement = ctx.ReadValue<Vector2>();
        _controls.GamePlay.Move.canceled += ctx => _playerMovement = Vector2.zero;
        // _controls.GamePlay.Jump.performed += ctx => Jump();
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

    void Start()
    {
        _characterController = new CharacterAnimationController(GetComponent<Animator>(), transform);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (_characterController.IsMidAnim()) return;
        Move(_playerMovement);
    }
    
    
    private void Move(Vector2 playerMovement)
    {
        if (playerMovement != Vector2.zero)
        {
            // check negative or positive to switch the character around
            gameObject.transform.localScale = new Vector3(playerMovement.x > 0 ? 1f : -1f, 1f, 1f);
        }

        _rigidbody2D.velocity = new Vector3(playerMovement.x * speed, playerMovement.y * speed);
        
        _characterController.ChangeAnimation(_rigidbody2D.velocity.x == 0
            ? _characterController.Idle
            : _characterController.Walk);
    }
}
