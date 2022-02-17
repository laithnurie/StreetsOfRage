using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Create new attack")]
public class Attack : ScriptableObject
{
    [SerializeField] private string attackName;
    [SerializeField] private int damage;
    [SerializeField] private int horizontalHitSpace;
    [SerializeField] private int verticalHitSpace;
    [SerializeField] private float cooldown;

    public string AttackName => attackName;

    public int Damage => damage;

    public int HorizontalHitSpace => horizontalHitSpace;

    public int VerticalHitSpace => verticalHitSpace;
    
    public float Cooldown => cooldown;
}