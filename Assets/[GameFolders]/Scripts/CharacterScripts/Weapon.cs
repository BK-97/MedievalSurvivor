using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public WeaponTrigger weaponTrigger;
    public ParticleSystem swordTrail;
    public void Initalize()
    {
        gameObject.SetActive(true);
    }
    public void Deactive()
    {
        gameObject.SetActive(false);
    }
    public void TrailPlay()
    {
        swordTrail.Play();
    }

}
