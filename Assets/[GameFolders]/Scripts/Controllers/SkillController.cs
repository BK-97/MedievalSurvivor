using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillController : MonoBehaviour
{
    public static UnityEvent OnPassiveSkillUse = new UnityEvent();
    public static UnityEvent OnPassiveSkillEnd = new UnityEvent();
    public static UnityEvent OnWeaponSkillUse = new UnityEvent();
    public GameObject passiveVFX;

    private float PASSIVE_TIME=10;
    private float PASSIVE_COOLDOWN=30;
    bool canUsePassive=true;
    private void OnEnable()
    {
        InputManager.OnPassiveSkillInput.AddListener(UsePassiveSkill);
        InputManager.OnWeaponSkillInput.AddListener(UseWeaponSkill);
    }
    private void OnDisable()
    {
        InputManager.OnPassiveSkillInput.RemoveListener(UsePassiveSkill);
        InputManager.OnWeaponSkillInput.RemoveListener(UseWeaponSkill);

    }
    private void UsePassiveSkill()
    {
        if (!canUsePassive)
            return;

        canUsePassive = false;
        passiveVFX.SetActive(true);
        Invoke("PassiveSkillOff", PASSIVE_TIME);
        OnPassiveSkillUse.Invoke();
    }
    private void UseWeaponSkill()
    {
        OnWeaponSkillUse.Invoke();
    }
    private void PassiveSkillOff()
    {
        passiveVFX.SetActive(false);
        OnPassiveSkillEnd.Invoke();
    }
}
