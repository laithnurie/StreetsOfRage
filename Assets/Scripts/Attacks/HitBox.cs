using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Attack currentAttack;

    public void SetAttack(Attack attack)
    {
        currentAttack = attack;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Check if other and damage 
        if (currentAttack != null)
        {
            //TODO: Light/Heavy attack logic based on attack detailed
        }
    }
}
