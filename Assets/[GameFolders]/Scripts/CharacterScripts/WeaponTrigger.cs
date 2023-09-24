using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    bool isTriggered;
    IDamagable contactedEnemy;
    //private void OnTriggerEnter(Collider other)
    //{
    //    IDamagable damagable = other.GetComponent<IDamagable>();
    //    if (damagable != null)
    //    {
    //        contactedEnemy = damagable;
    //        isTriggered = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (contactedEnemy==other.gameObject.GetComponent<IDamagable>())
    //    {
    //        contactedEnemy = null;
    //        isTriggered = false;
    //    }
    //}
   
    public bool IsContacted()
    {
        return isTriggered;
    }
    public IDamagable GetContacted()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Enemy"));

        float closestDistance = Mathf.Infinity;
        IDamagable closestEnemy = null;

        foreach (Collider col in colliders)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            IDamagable damagable = col.GetComponent<IDamagable>();

            if (distance < closestDistance && damagable != null)
            {
                closestDistance = distance;
                closestEnemy = damagable;
            }
        }

        // En yakýn düþmaný contactedEnemy olarak sakla
        contactedEnemy = closestEnemy;
        return contactedEnemy;
    }
}
