
using UnityEngine;

public class openDoor : MonoBehaviour {

    public bool isOpen = false;
    Quaternion openRot;
    Quaternion closedRot;
    float rotSpeed = 2.0f;

    private void Start()
    {
        openRot = Quaternion.Euler(0, -120, 0);
        closedRot = transform.localRotation;
    }

    private void Update()
    {
        if (!isOpen)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, closedRot, rotSpeed * Time.deltaTime);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, openRot, rotSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        isOpen = !isOpen;
    }
}
