using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : WeaponDamage
{
    // variables
    //[SerializeField] public bool canDmg = true;
    //[SerializeField] public float dmg = 1;
	
    // references
    Rigidbody rb;
    //[SerializeField] private LayerMask mask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("mask : " + mask.value + "gameobject.layer: " + other.gameObject.layer + "can attack : " + canDmg);
        if (base.canDmg)
        {
            if(base.mask == (mask | (1 << other.gameObject.layer)))//if included in the mask
            {
                // badguy bg = other.GetComponentInParent<badguy>();
                // if(bg != null)
                // {
                //     bg.Damage(dmg);
                //     canDmg = false;
                // }
                Debug.Log(this.name + ": ArrowHit :" + other.name);
                base.canDmg = false;
            }
        }

    }


}