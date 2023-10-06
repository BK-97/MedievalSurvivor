using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponChangeUI : MonoBehaviour
{
    public GameObject swordAndShield;
    public GameObject dualSwords;
    private GameObject currentIcon;
    private void OnEnable()
    {
        currentIcon = swordAndShield;
        currentIcon.SetActive(true);
        WeaponController.OnWeaponChange.AddListener(ChangeIcon);
    }
    private void OnDisable()
    {
        WeaponController.OnWeaponChange.RemoveListener(ChangeIcon);
    }
    private void ChangeIcon()
    {
        currentIcon.SetActive(false);
        GameObject newIcon=swordAndShield;
        if (currentIcon == swordAndShield)
            newIcon = dualSwords;
        else if(currentIcon == dualSwords)
            newIcon = swordAndShield;

        currentIcon = newIcon;
        currentIcon.SetActive(true);
        PunchIcon();
    }
    private void PunchIcon()
    {
        currentIcon.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 10, 1);
    }
}
