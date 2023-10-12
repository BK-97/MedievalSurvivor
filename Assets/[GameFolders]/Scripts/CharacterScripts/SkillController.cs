using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillController : MonoBehaviour
{
    #region Events
    public static UnityEvent OnPassiveSkillUse = new UnityEvent();
    public static UnityEvent OnPassiveSkillEnd = new UnityEvent();
    public static UnityEvent OnWeaponSkillUse = new UnityEvent();
    public static FloatEvent OnSendCooldownInfo = new FloatEvent();
    #endregion
    #region Params
    public GameObject passiveVFX;

    private const float PASSIVE_TIME = 10;
    private const float PASSIVE_COOLDOWN = 30;
    private const float WEAPON_SKILL_COOLDOWN = 30;
    [HideInInspector]
    public float weaponSkillRadius = 5;
    bool canUsePassive = true;
    bool canUseWeaponSkill = true;
    #endregion
    #region Methods

    public void UsePassiveSkill()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        if (!canUsePassive)
            return;

        canUsePassive = false;
        passiveVFX.SetActive(true);
        Invoke("PassiveSkillOff", PASSIVE_TIME);
        Invoke("PassiveSkillUsable", PASSIVE_COOLDOWN);
        ObjectPoolManager.SpawnObject(ObjectPoolManager.Instance.GetObjectFromName("FSkillFX"), transform.position, Quaternion.identity);
        OnPassiveSkillUse.Invoke();
        OnSendCooldownInfo.Invoke(PASSIVE_COOLDOWN);
    }
    public void UseWeaponSkill()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        if (!canUseWeaponSkill)
            return;

        canUseWeaponSkill = false;
        Invoke("WeaponSkillUsable", WEAPON_SKILL_COOLDOWN);
        OnWeaponSkillUse.Invoke();
        OnSendCooldownInfo.Invoke(WEAPON_SKILL_COOLDOWN);
    }
    private void PassiveSkillOff()
    {
        passiveVFX.SetActive(false);
        OnPassiveSkillEnd.Invoke();
    }
    private void PassiveSkillUsable()
    {
        canUsePassive = true;
    }
    private void WeaponSkillUsable()
    {
        canUseWeaponSkill = true;
    }
    #endregion
}
