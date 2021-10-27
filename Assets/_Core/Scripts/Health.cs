using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Varibles
    [SerializeField] private bool life;
    [SerializeField] private bool body;
    [SerializeField] private LayerMask damageMask;

    // References
    [SerializeField] private Collider hitbox;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private GameObject animatedModel;

    void Start()
    {
        hitbox = GetComponentInChildren<Collider>();
    }

    void Update()
    {
       
    }

    void Hit()
    {
        ToggleBody();
    }

    [ContextMenu("ToggleBody")]
    void ToggleBody()
    {
        body = !body;

        if(!body)
        {
            CopyTransformData(animatedModel.transform, ragdoll.transform);
            ragdoll.gameObject.SetActive(true);
            animatedModel.SetActive(false);
        }
        else
        {
            CopyTransformData(ragdoll.transform, animatedModel.transform/*,navmeshAgent velocity*/);
            ragdoll.gameObject.SetActive(false);
            animatedModel.SetActive(true);
        }
    }

    private void CopyTransformData(Transform sourceTransform, Transform destinationTransform/*,Vector3 velocity*/)
    {
        if (sourceTransform.childCount != destinationTransform.childCount)
        {
            Debug.LogWarning("Invalid tranform copy, they need to match transform hierarchies");
            return;
        }

        for(int i = 0; i < sourceTransform.childCount; i++)
        {
            var source = sourceTransform.GetChild(i);
            var destination = destinationTransform.GetChild(i);
            destination.position = source.position;
            destination.rotation = source.rotation;
            var rb = destination.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = Vector3.zero; //velocity;

            CopyTransformData(source, destination);
        }
    
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hitbox Collision with " + other.name);
        if(damageMask == (damageMask | (1 << other.gameObject.layer)))//if included in the mask
        {  
            //Debug.Log(".. Collision ");
            WeaponDamage weapon = other.gameObject.GetComponentInChildren<WeaponDamage>();
            
            if(weapon.canDmg && weapon.dmg > 0)
            {
                Debug.Log(this.name + "!!! I'M HIT by: " + other.gameObject.name);
                weapon.dmg = 0;
                Hit();
            }
        }
    }
}
