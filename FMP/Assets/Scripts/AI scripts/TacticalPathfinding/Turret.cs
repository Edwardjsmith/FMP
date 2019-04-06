using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    const float range = 0.22f;
    GameObject target;

    GameObject turret;
	// Use this for initialization
	void Start ()
    {
        transform.GetComponentInChildren<SphereCollider>().radius = range / 2;
        target = GameObject.Find("Agent");
        turret = transform.Find("turretRot").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*Vector3 direction =  target.transform.position - turret.transform.position;
 
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 100 * Time.deltaTime);*/
       
	}
}
