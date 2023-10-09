using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Datas/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Datas")]
    public WeaponTypes WeaponType;

    public float WeaponDamage;
    public float AttackRange;

}
