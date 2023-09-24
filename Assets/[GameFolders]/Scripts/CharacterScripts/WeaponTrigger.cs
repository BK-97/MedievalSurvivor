using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    bool isTriggered;
    IDamagable contactedEnemy;
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            contactedEnemy = damagable;
            isTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (contactedEnemy==other.gameObject.GetComponent<IDamagable>())
        {
            contactedEnemy = null;
            isTriggered = false;
        }
    }
    public bool IsContacted()
    {
        return isTriggered;
    }
    public IDamagable GetContacted()
    {
        return contactedEnemy;
    }
}
