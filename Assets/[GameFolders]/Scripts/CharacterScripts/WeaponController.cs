using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponController : MonoBehaviour
{
    public WeaponHolder leftHandHolder;
    public WeaponHolder rightHandHolder;
    private CharacterStateController stateController;
    public List<WeaponTypes> usableWeapons;
    private WeaponTypes currentWeaponType;
    public static UnityEvent OnWeaponChange = new UnityEvent();

    private void Start()
    {
        stateController = GetComponent<CharacterStateController>();
    }

    private void OnEnable()
    {
        OnWeaponChange.AddListener(ChangeWeapon);
        LevelManager.Instance.OnLevelStart.AddListener(SetWeapon);
    }
    private void OnDisable()
    {
        OnWeaponChange.RemoveListener(ChangeWeapon);
        LevelManager.Instance.OnLevelStart.RemoveListener(SetWeapon);

    }
    public void ChangeWeapon()
    {
        if (stateController.AnimController.IsAttacking())
            return;
        if (stateController.AnimController.IsRolling())
            return;
        if (stateController.AnimController.IsOnSkillAnim())
            return;
        if (usableWeapons[usableWeapons.Count - 1] == currentWeaponType)
            currentWeaponType = usableWeapons[0];
        else
        {
            int currentIndex = (int)currentWeaponType;
            currentIndex++;
            currentWeaponType = usableWeapons[currentIndex];
        }
        SetWeapon();
    }
    private void SetWeapon()
    {
        leftHandHolder.WeaponChange((int)currentWeaponType);
        rightHandHolder.WeaponChange((int)currentWeaponType);
        stateController.AnimController.SetWeaponIndex((int)currentWeaponType);

        SetDamage();
    }
    public bool CheckForContact()
    {
        return leftHandHolder.currentWeapon.weaponTrigger.IsContacted();
    }
    public void SetDamage()
    {
        float totalDamage = 0;
        totalDamage += leftHandHolder.GetCurrentWeaponData().WeaponDamage + rightHandHolder.GetCurrentWeaponData().WeaponDamage;
        float attackRange = rightHandHolder.GetCurrentWeaponData().AttackRange;
        stateController.AttackController.SetAttackData(totalDamage, attackRange);
    }
    public IDamagable GetTriggeredDamagable()
    {
        return rightHandHolder.currentWeapon.weaponTrigger.GetContacted();
    }
}
