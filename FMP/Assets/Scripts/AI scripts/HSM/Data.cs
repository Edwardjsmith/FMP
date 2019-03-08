using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Data : MonoBehaviour
{
    NavMeshAgent agent;

    public float sightRange;
    public float fieldOfView;
    public float health;
    public float speed;

    public float safeDistance;
    public GameObject enemyTarget;
    public GameObject coverTarget;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    
}
