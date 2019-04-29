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
        targetPos = controller.targetPos;
        anim.speed = speed / 3;
        roam();
    }

    public void roam()
    {
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(rotationTarget),
                                                      rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                flock();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void flock()
    {
        relativePos = controller.averagePos + (targetPos - transform.position);
        speed = 3.0f;

        Vector3 direction = (relativePos + controller.seperation) - transform.position;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "flock")
        {
            rotationTarget = transform.position - other.gameObject.transform.position;
            float scaredSpeed = speed * 2;
            speed = scaredSpeed;
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







