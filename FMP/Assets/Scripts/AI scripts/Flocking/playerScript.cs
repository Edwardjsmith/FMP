using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : flockEntity
{
    Animator anim;
    bool turning = false;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        anim.Play("Swim");
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        var y = Input.GetAxis("Horizontal");
        var x = Input.GetAxis("Vertical");

        transform.Rotate(x, y, 0);
	}

}
