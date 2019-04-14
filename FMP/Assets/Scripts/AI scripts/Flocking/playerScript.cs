using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerScript : flockEntity
{
    Animator anim;

    public Camera[] cameras;
    int camIndex = 0;

    Vector3 rotationTarget;
    Vector3 targetPos;

    bool turning = false;
    bool shark = true;

    float updateTimer = 8.0f;
    
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        anim.Play("Swim");
        speed = 10.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if(camIndex == 0)
        {
            shark = true;
        }
        else
        {
            shark = false;
        }

        /*if(Input.GetButtonDown("Fire1"))
        {
            if (camIndex + 1 < cameras.Length)
            {
                cameras[camIndex].gameObject.SetActive(false);
                camIndex++;
                cameras[camIndex].gameObject.SetActive(true);
            }
            else
            {
                camIndex = 0;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (camIndex - 1 > 0)
            {
                cameras[camIndex].gameObject.SetActive(false);
                camIndex--;
                cameras[camIndex].gameObject.SetActive(true);
            }
            else
            {
                camIndex = cameras.Length - 1;
            }
        }*/

        if (shark)
        {
            if (!turning)
            {
                var y = Input.GetAxis("Horizontal");
                var x = Input.GetAxis("Vertical");
                var z = Input.GetAxis("Mouse X") * 5;

                transform.Rotate(x, y, z);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(rotationTarget),
                                                      rotationSpeed * Time.deltaTime);
            }
        }
        else
        { 
            if (updateTimer <= 0)
            {
                targetPos = new Vector3(Random.Range(-range, range),
                            10,
                                Random.Range(-range, range));
                updateTimer = 8.0f;
            }

            updateTimer -= Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(targetPos),
                                                        rotationSpeed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bounds")
        {
            rotationTarget = transform.position - other.gameObject.transform.position;
            //turning = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bounds")
        {
            //turning = false;
        }
    }
}
