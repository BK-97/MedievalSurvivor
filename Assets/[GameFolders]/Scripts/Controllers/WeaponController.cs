using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponController : MonoBehaviour
{
    private WeaponHolder weaponHolder;
    private int currentWeaponIndex=-1;
    StateController stateController;

    public static UnityEvent OnWeaponChange = new UnityEvent();

    private void Start()
    {
        stateController = GetComponent<StateController>();
        weaponHolder = GetComponentInChildren<WeaponHolder>();
        ChangeWeapon();
    }
    private void OnEnable()
    {
        OnWeaponChange.AddListener(ChangeWeapon);
    }
    private void OnDisable()
    {
        OnWeaponChange.RemoveListener(ChangeWeapon);

    }
    public void ChangeWeapon()
    {
        currentWeaponIndex++;
        if (weaponHolder.Weapons.Count == currentWeaponIndex)
            currentWeaponIndex = 0;

        weaponHolder.WeaponChange(currentWeaponIndex);

        SetDamage();

        stateController.AnimController.SetWeaponIndex(currentWeaponIndex);
    }
    public void SetDamage()
    {
        stateController.AttackController.SetAttackData(weaponHolder.GetCurrentWeaponDamage());
    }
}
