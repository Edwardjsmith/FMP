using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setControllerValues : MonoBehaviour
{
    public float flockRange;
    public int flockPopulation;
    public float seperationDistance;

    public GameObject[] otherAI;

    private void Start()
    {
        foreach(GameObject agent in otherAI)
        {
            Instantiate(agent, transform);
        }
    }
}
