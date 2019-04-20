﻿
using UnityEngine;

public class FPSBody : gameEntity {

    public float movementSpeed = 10;
    CharacterController controller;
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
        float vertInput = Input.GetAxis("Vertical") * movementSpeed;

        getAnim().SetFloat("velX", horizInput);
        getAnim().SetFloat("velY", vertInput);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        controller.SimpleMove(forwardMovement + rightMovement);

        FPSInput();
    }

    void FPSInput()
    {
        if (GetWeapon() != null)
        {
            if (Input.GetMouseButton(0) && GetWeapon().ammo > 0 && !GetWeapon().reloading)
            {
                getAnim().SetBool("transitionToShooting", true);
                GetWeapon().Fire();
            }
            else if (GetWeapon().ammo <= 0)
            {
                GetWeapon().reload(this);
            }
        }
    }
}
