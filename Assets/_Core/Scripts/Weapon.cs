using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : WeaponDamage
{
//     public LayerMask mask;
//     public float base.dmg;
//     public bool base.canDmg;
    public float attackDuriation;
    private float timeSinceAttack;
    public bool canAttack;

    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if(timeSinceAttack > attackDuriation)
        {
            base.canDmg = false;
            base.dmg = 1;
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    public void Attack()
    {
        //Debug.Log("!! ATTACK-BEGIN !!");
        canAttack = false;
        base.canDmg = true;
        timeSinceAttack = 0;
    }

    public void StopAttack()
    {
        //Debug.Log("00 ATTACK-END 00");
        base.canDmg = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("mask : " + mask.value + "gameobject.layer: " + other.gameObject.layer + "can attack : " + base.canDmg);
        if (base.canDmg)
        {
            if(mask == (mask | (1 << other.gameObject.layer)))//if included in the mask
            {
                // badguy bg = other.GetComponentInParent<badguy>();
                // if(bg != null)
                // {
                //     bg.Damage(base.dmg);
                //     base.canDmg = false;
                // }
                Debug.Log(this.name + ": WeaponHit :" + other.name);
            }
        }

    }
}
