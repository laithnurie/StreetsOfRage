using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController
{
    private CharacterAnimationController _characterController;
    private float lastProceedAttack;
    private Queue<Attack> AttacksQueues;
    private bool midAttack;
    private readonly int _maxContinuousAttack = 3;

    public AttackController(CharacterAnimationController characterController)
    {
        _characterController = characterController;
        AttacksQueues = new Queue<Attack>(_maxContinuousAttack);
    }

    public bool NeedToProcessAttack() => AttacksQueues.Count != 0 && !midAttack;

    public void AddAttack(Attack attack)
    {
        if (AttacksQueues.Count == _maxContinuousAttack) return;
        AttacksQueues.Enqueue(attack);
    }

    public IEnumerator NextAttack()
    {
        yield return ProcessAttack(AttacksQueues.Dequeue());
    }

    private IEnumerator ProcessAttack(Attack attack)
    {
        midAttack = true;
        yield return _characterController.AnimateOnce(attack.AttackType, true, () => { midAttack = false; });
    }
}