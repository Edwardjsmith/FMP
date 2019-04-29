using UnityEngine;

public class sharkCollision : MonoBehaviour {

    flockPlayer player;
    public LayerMask ground;

    Vector3 fHit;
    Vector3 bHit;

    float rotSpeed = 10.0f;
    // Use this for initialization
    void Start ()
    {
        player = transform.parent.GetComponent<flockPlayer>();
	}

    private void Update()
    {
        checkFloor();
    }

    void checkFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position + transform.forward, -transform.up, out hit, ground))
        {
            fHit = hit.point; // get the point where the front ray hits the ground
            if (Physics.Raycast(player.transform.position - transform.forward, -transform.up, out hit, ground))
            {
                bHit = hit.point; // get the back hit point
                float distToFloor = Vector3.Distance(player.transform.position, fHit);

                if (distToFloor < 3.0f)
                {
                    player.setAvoidFloor(true);
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation,
                                            Quaternion.LookRotation(fHit - bHit),
                                              (rotSpeed / distToFloor) * Time.deltaTime);

                    player.transform.position += player.transform.up * Time.deltaTime;
                }
                else
                {
                    player.setAvoidFloor(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bounds")
        {
            player.setTurning(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bounds")
        {
            player.setTurning(false);
        }
    }
}
