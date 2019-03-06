using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : gameEntity
{
    private CharacterController Controller;

    public float mouseSensitivity;
    float xAxisClamp = 0.0f;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotateView();
        movement();

        if(Input.GetMouseButton(0))
        {
            GetWeapon().Fire();
            Debug.Log("Fire");
        }
    }

    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
       

        //Movement

        //Move side to side
        Vector3 moveHorizontal = transform.right * horizontal * speed * Time.deltaTime;

        //Move forward and back
        Vector3 moveForward = transform.forward * vertical * speed * Time.deltaTime;

        //Implement said moves
        Controller.SimpleMove(moveHorizontal);
        Controller.SimpleMove(moveForward);
    }

    void rotateView()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotationCamera = transform.rotation.eulerAngles;

        //targetRotationCamera.x -= rotAmountY; //Rotatates camera in direction of cursor up and down
        targetRotationCamera.z = 0; //Stops over-rotation;
        targetRotationCamera.y += rotAmountX; // Rotates whole body to make movement easier


        // Clamps the camera in the x between -90 and 90 (directly up and directly down)
        //To stop flicker
        if (xAxisClamp > 90)
        {
            xAxisClamp = targetRotationCamera.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotationCamera.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotationCamera);

    }
}
