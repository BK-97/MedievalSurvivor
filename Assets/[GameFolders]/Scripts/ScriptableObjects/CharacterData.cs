using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Datas/CharacterData")]
public class CharacterData : ScriptableObject
{
    public CharacterTypes CharacterType;

    [Header("Health")]
    public float Health;

    [Header("Movement")]
    public int MoveSpeed;

    [Header("Attack")]
    public float BaseDamage;
    public float AttackRange;

}
