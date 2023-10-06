using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SkillUISlider : MonoBehaviour
{
    public Slider timeSlider;
    public Image clickInfo;
    public bool weaponSkill;
    private void OnEnable()
    {
        SkillController.OnWeaponSkillUse.AddListener(CheckForWeaponSkill);
        SkillController.OnPassiveSkillUse.AddListener(CheckForPassiveSkill);
        SkillController.OnSendCooldownInfo.AddListener(SetSliderTimer);
    }
    private void OnDisable()
    {
        SkillController.OnWeaponSkillUse.RemoveListener(CheckForWeaponSkill);
        SkillController.OnPassiveSkillUse.RemoveListener(CheckForPassiveSkill);
        SkillController.OnSendCooldownInfo.RemoveListener(SetSliderTimer);
    }
    private void Start()
    {
        timeSlider.value = timeSlider.maxValue;
    }
    private void CheckForWeaponSkill()
    {
        if (weaponSkill)
        {
            timeSlider.value = 0;
            PunchClickInfo();
        }
    }
    private void CheckForPassiveSkill()
    {
        if (!weaponSkill)
        {
            timeSlider.value = 0;
            PunchClickInfo();
        }
    }
    private void SetSliderTimer(float totalTime)
    {
        if(timeSlider.value==0)
            timeSlider.DOValue(timeSlider.maxValue, totalTime).OnComplete(PunchClickInfo);

    }
    private void PunchClickInfo()
    {
        clickInfo.transform.DOPunchScale(Vector3.one*0.2f, 0.3f, 10, 1);
    }
}
