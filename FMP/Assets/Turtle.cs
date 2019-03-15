using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{

    Vector3 target;
    float speed = 5.0f;
    float rotationSpeed = 1.0f;

    public GameObject[] targets;

    // Use this for initialization
    void Start ()
    {
        targets = GameObject.FindGameObjectsWithTag("Waypoint");
        target = targets[Random.Range(0, targets.Length - 1)].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(target),
                                                      rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Waypoint")
        {
            target = targets[Random.Range(0, targets.Length - 1)].transform.position;
        }
    }
}
