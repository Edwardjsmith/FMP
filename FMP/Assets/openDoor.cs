
using UnityEngine;

public class openDoor : MonoBehaviour {

    public bool isOpen = false;
    Quaternion openRot;
    Quaternion closedRot;
    float rotSpeed = 2.0f;

    Vector3 away;
    LayerMask doorLayer;

    float doorTimer = 0;
    private void Start()
    {
        closedRot = transform.localRotation;
        doorLayer = gameObject.layer;
    }

    private void Update()
    {
        doorTimer -= Time.deltaTime;
        if (!isOpen)
        {
            gameObject.layer = doorLayer;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, closedRot, rotSpeed * Time.deltaTime);
            
        }
        else
        {
            gameObject.layer = 0;
            if (away.x > transform.position.x)
            {
                openRot = Quaternion.Euler(0, 120, 0);
            }
            else
            {
                openRot = Quaternion.Euler(0, -120, 0);
            }

            transform.localRotation = Quaternion.Slerp(transform.localRotation, openRot, rotSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor(Vector3 rotateFrom)
    {
        if (doorTimer <= 0)
        {
            away = rotateFrom;
            isOpen = !isOpen;
            doorTimer = 2.0f;
        }
    }
}
