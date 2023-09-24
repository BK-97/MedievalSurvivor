using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponController : MonoBehaviour
{
    public WeaponHolder weaponHolder;
    private int currentWeaponIndex=-1;
    private CharacterStateController stateController;

    public static UnityEvent OnWeaponChange = new UnityEvent();

    private void Start()
    {
        stateController = GetComponent<CharacterStateController>();
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
        stateController.AnimController.SetWeaponIndex(currentWeaponIndex);

        SetDamage();

    }
    public bool CheckForContact()
    {
        return weaponHolder.currentWeapon.weaponTrigger.IsContacted();
    }
    public void SetDamage()
    {
        stateController.AttackController.SetAttackData(weaponHolder.GetCurrentWeaponDamage());
    }
    public IDamagable GetTriggeredDamagable()
    {
        return weaponHolder.currentWeapon.weaponTrigger.GetContacted();
    }
}
