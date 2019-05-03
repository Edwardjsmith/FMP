using UnityEngine;

public class flockBehaviour : flockEntity
{
    bool turning = false;
    public Vector3 targetPos;

    Animator anim;
    public flockController controller;
    Vector3 rotationTarget;
    Vector3 relativePos = Vector3.zero;

    public float seperationDistance = 16.0f;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Swim");
    }

    // Update is called once per frame
    void Update()
    {
        //Set target to controller target and apply roaming
        targetPos = controller.targetPos;
        anim.speed = speed / 3;
        roam();
    }

    public void roam()
    {

        if (turning)
        {   //Rotating away from target applied here
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(rotationTarget),
                                                      rotationSpeed * Time.deltaTime);
        }
        else
        {
            //Apply flocking at random intervals
            if (Random.Range(0, 5) < 1)
            {
                flock();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void flock()
    {
        //get relative position to flock
        relativePos = controller.averagePos + (targetPos - transform.position);
        speed = 3.0f;

        //Apply flocking
        Vector3 direction = (relativePos + controller.seperation) - transform.position;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "flock")
        {
            rotationTarget = transform.position - other.gameObject.transform.position;
            speed = 6.0f;
            rotationSpeed = 5;
            turning = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "flock")
        {
            rotationSpeed = 1;
            speed = 3.0f;
            turning = false;
        }
    }
}







