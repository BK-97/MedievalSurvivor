using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public List<Weapon> Weapons;
    public Weapon currentWeapon;

    public void WeaponChange(int index)
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].Deactive();
        }
        currentWeapon = Weapons[index];
        currentWeapon.Initalize();
    }
    public float GetCurrentWeaponDamage()
    {
        return currentWeapon.weaponData.WeaponDamage;
    }
}
