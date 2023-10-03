using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    float currentDamage;
    public Transform muzzle;
    public LayerMask enemyLayer;
    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<CharacterAnimationController>() : animController; } }
    private WeaponController weaponController;
    public WeaponController WeaponController { get { return (weaponController == null) ? weaponController = GetComponent<WeaponController>() : weaponController; } }
    public void SetAttackData(float damage)
    {
        currentDamage = damage;
    }
    public void Attack(bool status)
    {
        Vector3 attackDirection = InputManager.Instance.GetMouseWorldPos() - transform.position;
        if (!AnimController.IsAttacking())
        {
            if (attackDirection != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(attackDirection);
                transform.rotation = rotation;
            }
        }
        if (status)
        {
            AnimController.AttackAnimation();

            if (AnimController.canTakeCombo)
            {
                AnimController.ComboAttack();
            }
        }
        else
        {
            AnimController.EndAttack();
        }
    }

    public void GiveDamage(IDamagable enemyTarget, float damage)
    {
        enemyTarget.TakeDamage(damage);
    }
    public void AttackMoment()
    {
        WeaponController.weaponHolder.currentWeapon.TrailPlay();
        if (RaycastCheck() != null)
        {
            GiveDamage(RaycastCheck(), currentDamage);
        }
    }
    private IDamagable RaycastCheck()
    {
        Vector3 coneDirection = muzzle.transform.forward;
        Quaternion startRotation = Quaternion.AngleAxis(-15f, Vector3.up);
        Quaternion stepRotation = Quaternion.AngleAxis(30f / (12 - 1), Vector3.up);
        List<GameObject> hitObjects = new List<GameObject>();
        for (int i = 0; i < 12; i++)
        {
            Vector3 rayStartPoint = muzzle.transform.position;

            Vector3 rayDirection = coneDirection;

            rayDirection = startRotation * rayDirection;

            RaycastHit hit;
            if (Physics.Raycast(rayStartPoint, rayDirection, out hit, 2, enemyLayer))
            {
                if (hit.collider.gameObject.GetComponent<IDamagable>() != null)
                    hitObjects.Add(hit.collider.gameObject);
            }

            startRotation = stepRotation * startRotation;
        }

        IDamagable closestObject = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject hit in hitObjects)
        {
            float distanceToMuzzle = Vector3.Distance(muzzle.position, hit.transform.position);
            if (distanceToMuzzle < closestDistance)
            {
                closestDistance = distanceToMuzzle;
                closestObject = hit.GetComponent<IDamagable>();
            }
        }
        return closestObject;
    }
}
