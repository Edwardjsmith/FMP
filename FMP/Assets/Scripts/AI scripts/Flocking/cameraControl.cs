
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    public flockController controller;
	void Update ()
    {
        transform.position = controller.flockAgents[0].transform.position;
        controller.flockAgents[0].gameObject.GetComponentInChildren<Renderer>().enabled = false;

    }
}
