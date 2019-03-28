using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    const float range = 0.2f;
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
        Vector3 direction = turret.transform.position - target.transform.position;
 
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rot = lookRotation.eulerAngles;
        turret.transform.rotation = Quaternion.Euler(0, rot.y, 0);
	}
}
