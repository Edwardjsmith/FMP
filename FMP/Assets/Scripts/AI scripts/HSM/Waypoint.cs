using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject[] otherPoints;
    bool guard = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "entity")
        {
            GameObject go = other.transform.gameObject;
            go.SendMessage("updatePath", otherPoints[Random.Range(0, otherPoints.Length - 1)]);
        }
    }

    public bool Guard()
    {
        return guard;
    }
}
