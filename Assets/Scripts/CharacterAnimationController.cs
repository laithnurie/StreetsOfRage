using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimationController
{
    private readonly Animator _animator;
    private readonly Transform _transform;

    private string _state;
    private bool _isMidAnim = false;

    public CharacterAnimationController(Animator animator, Transform transform)
    {
        _animator = animator;
        _transform = transform;
        _state = Idle;
    }

    public void ChangeAnimation(string newAnimation)
    {
        if (CouldAnimate(newAnimation)) return;
        _animator.Play(newAnimation);
        _state = newAnimation;
    }

    public IEnumerator AnimateOnce(string newAnimation, bool idle, Action<float> rigidBodyMovement = null,
        Action<float> onComplete = null, float additionalWait = 0f)
    {
        if (_state == newAnimation) yield return null;
        _animator.Play(newAnimation);
        _state = newAnimation;


        var direction = _transform.localScale.x;
        rigidBodyMovement?.Invoke(direction);

        var length = GetAnimationLength(newAnimation);
        _isMidAnim = true;
        yield return new WaitForSeconds(length + additionalWait);
        _isMidAnim = false;
        //TODO: include running
        ChangeAnimation(idle ? Idle : Walk);
        onComplete?.Invoke(direction);
    }

    public IEnumerator DeathAnimation()
    {
        ChangeAnimation(Death);
        var length = GetAnimationLength(Death);
        _isMidAnim = true;
        yield return new WaitForSeconds(length);
        _isMidAnim = false;
        _animator.StopPlayback();
    }

    private float GetAnimationLength(string animationName)
    {
        var time = 1.79f;
        RuntimeAnimatorController ac = _animator.runtimeAnimatorController; //Get Animator controller
        foreach (var clip in ac.animationClips)
        {
            if (clip.name != animationName) continue;
            time = clip.length;
            return time;
        }

        return time;
    }

    private bool CouldAnimate(string newAnimation) => _state != newAnimation && _state == Death;

    public bool IsMidAnim() => _isMidAnim;
    public string CurrentAnimation() => _state;

    public string Idle => "Idle";
    public string Walk => "Walk";
    public static string Run => "Run";
    public static string Fall => "Fall";
    public static string Jump => "Jump";
    public string Attack1 => "Attack1";
    public string Attack2 => "Attack2";
    // public static string Roll => "Roll";
    
    // public static string Dash => "Dash";
    // public static string Attack => "Attack";
    // public static string JumpAttack => "JumpAttack";
    // public static string SpecialAttack => "SpecialAttack";
    // public static string Hit => "Hit";
    public static string Death => "Death";
}