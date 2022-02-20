using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    private CharacterAnimationController _characterController;
    [SerializeField] private float speed = 10f;
    
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _characterController = new CharacterAnimationController(GetComponent<Animator>(), transform);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void UpdateBody(Vector2 playerMovement)
    {
        if (_characterController.IsMidAnim()) return;
        Move(playerMovement);
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


}
