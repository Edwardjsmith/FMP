using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    public flockController controller;
	void Update ()
    {
        transform.position = controller.flockAgents[0].transform.position;
	}
}
