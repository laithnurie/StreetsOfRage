using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController
{
    private readonly CharacterAnimationController _characterController;
    private readonly HitBox _hitBox;
    private readonly AudioSource _playerAudioSource;
    private float _lastProceedAttack;
    private readonly Queue<Attack> _attacksQueues;
    private bool _midAttack;
    private readonly int _maxContinuousAttack = 3;

    public AttackController(CharacterAnimationController characterController, HitBox hitBox,
        AudioSource playerAudioSource)
    {
        _characterController = characterController;
        _hitBox = hitBox;
        _playerAudioSource = playerAudioSource;
        _attacksQueues = new Queue<Attack>(_maxContinuousAttack);
    }

    public bool NeedToProcessAttack() => _attacksQueues.Count != 0 && !_midAttack;

    public void AddAttack(Attack attack)
    {
        if (_attacksQueues.Count == _maxContinuousAttack) return;
        _attacksQueues.Enqueue(attack);
    }

    public IEnumerator NextAttack()
    {
        yield return ProcessAttack(_attacksQueues.Dequeue());
    }

    private IEnumerator ProcessAttack(Attack attack)
    {
        _midAttack = true;
        _hitBox.SetAttack(attack);
        if (attack.AttackSound != null)
        {
            _playerAudioSource.PlayOneShot(attack.AttackSound);    
        }
        
        yield return _characterController.AnimateOnce(attack.AttackType, true, () =>
        {
            _midAttack = false;
            _hitBox.SetAttack(null);
        });
    }
}