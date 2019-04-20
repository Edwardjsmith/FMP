using UnityEngine;

public class FPSCam : MonoBehaviour
{
    float mouseSensitivity = 100.0f;
    float xAxisClamp = 0;
    Transform playerBody;
    // Use this for initialization
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent;
    }

    private void Update()
    {
        camRotation();
    }

    void camRotation()
    {
        float mouseX = -Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxis(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxis(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void ClampXAxis(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
