using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundCreature : flockEntity
{

    public Vector3 target;
    Vector3 rotationTarget;
    bool turning = false;
    float updateTimer = 8.0f;

    // Use this for initialization
    void Start ()
    {
        target = new Vector3(Random.Range(-range, range),
                        10,
                            Random.Range(-range, range));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(rotationTarget),
                                                          rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(target),
                                                      rotationSpeed * Time.deltaTime);
        }

        if(updateTimer <= 0)
        {
            target = new Vector3(Random.Range(-range, range),
                        10,
                            Random.Range(-range, range));
            updateTimer = 8.0f;
        }

        updateTimer -= Time.deltaTime;

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "entity")
        {
            turning = true;
            rotationTarget = transform.position - other.transform.position;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "entity")
        {
            turning = false;
        }
    }
}
