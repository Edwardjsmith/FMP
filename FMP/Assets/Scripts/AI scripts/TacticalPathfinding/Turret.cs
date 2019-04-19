using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    const float range = 0.22f;

	// Use this for initialization
	void Start ()
    {
        transform.GetComponentInChildren<SphereCollider>().radius = range / 2;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
