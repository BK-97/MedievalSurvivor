using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponController : MonoBehaviour
{
    public WeaponHolder WeaponHolder;
    private int currentWeaponIndex=-1;
    StateController stateController;

    public static UnityEvent OnWeaponChange = new UnityEvent();

    private void Start()
    {
        stateController = GetComponent<StateController>();
        WeaponHolder = GetComponentInChildren<WeaponHolder>();
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
        if (WeaponHolder.Weapons.Count == currentWeaponIndex)
            currentWeaponIndex = 0;

        WeaponHolder.WeaponChange(currentWeaponIndex);

        SetDamage();
        Debug.Log(currentWeaponIndex);
        stateController.AnimController.SetWeaponIndex(currentWeaponIndex);
    }
    public void SetDamage()
    {
        stateController.AttackController.SetAttackData(WeaponHolder.GetCurrentWeaponDamage());
    }
}
