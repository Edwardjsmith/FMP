﻿
using UnityEngine;

public class flockPlayer : flockEntity
{
    Animator anim;

    Vector3 rotationTarget;
    Vector3 targetPos;

    bool turning = false;
    public bool avoidFloor = false;

    float turnSpeed = 80.0f;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        anim.Play("Swim");
        speed = 10.0f;
        rotationTarget = GameObject.Find("boat").transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (!turning && !avoidFloor)
        {
            //While not avoiding boundaries, accept movement
            var y = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
            var x = Input.GetAxis("Vertical") * turnSpeed * Time.deltaTime;
            var z = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;

            transform.Rotate(x, y, z);
        }
        else if(turning)
        {
            //Turn away from boundary
            Vector3 rotation = rotationTarget - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                Quaternion.LookRotation(rotation),
                                                  (rotationSpeed) * Time.deltaTime);
        }
    }
       
    public void setTurning(bool set)
    {
        turning = set;
    }

    public void setAvoidFloor(bool set)
    {
        avoidFloor = set;
    }
}
