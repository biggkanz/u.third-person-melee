using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowPlace;
    [SerializeField] private GameObject playerCamera;
    private Animator playerAnim;
    private int aimBool;

    private void Awake()
    {
        // references
        aimBool = Animator.StringToHash("Aim");
        playerAnim = GetComponent<Animator>();
        
        // checks
        if (arrowPrefab == null)
        {
            Debug.LogError("arrowPrefab is NULL; Awake(); ArrowShooter; ");
            //arrowPrefab = Resources.Load ("ArrowProjectile") as GameObject;
        }
        if (arrowPlace == null)
            Debug.LogError("arrowPlace is NULL; Awake(); ArrowShooter;");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (arrowPlace == null)
        {
            Debug.LogError("arrowPlace is NULL; Start(); ArrowShooter;");
            var ap = new GameObject();
            ap.transform.position = Vector3.zero;
            arrowPlace = ap;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown(0) && playerAnim.GetBool(aimBool))
        {
        	GameObject newArrow = Instantiate(arrowPrefab) as GameObject;
        	newArrow.transform.position = arrowPlace.transform.position;
            newArrow.transform.rotation = arrowPlace.transform.rotation;
        	Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        	rb.velocity = /*arrowPlace.transform.position +*/ playerCamera.transform.forward*30;
        }
    }
}