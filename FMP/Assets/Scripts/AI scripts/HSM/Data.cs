
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Data : MonoBehaviour
{
    NavMeshAgent agent;

    public float sightRange;
    public float fieldOfView;
    public float health;
    public float speed;
    public float attackDistance;
    public float doorOpenRange;

    public float safeDistance;

    public GameObject enemyTarget;
    public Vector3 coverTarget;

    public GameObject doorTarget;

    public GameObject potentialTarget;

    public float runSpeed = 4f;
    public float walkSpeed = 2f;

    List<GameObject> cover;
    GameObject[] findCover;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        findCover = GameObject.FindGameObjectsWithTag("Cover");
        cover = findCover.ToList();
        Debug.Log(cover.Count);
        speed = walkSpeed;
    }

    public List<GameObject> getCover()
    {
        return cover;
    }
	
	// Update is called once per frame
	void Update ()
    {
        agent.speed = speed;
	}

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    
}
