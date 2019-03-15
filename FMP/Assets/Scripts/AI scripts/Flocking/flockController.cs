using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class flockController : MonoBehaviour
{

    setControllerValues values;
    public GameObject[] fishPrefab;

    int flockPopulation;
    public float flockRange;

    public GameObject[] flockAgents;
    public GameObject[] totalAgents;
    public Vector3 goalPos = Vector3.zero;

    public GameObject[] waypoints;

    float updateTimer = 5.0f;
    float seperationDistance;

	// Use this for initialization
	void Start ()
    {
        values = FindObjectOfType<setControllerValues>();
        flockRange = values.flockRange;
        flockPopulation = values.flockPopulation;

        flockAgents = new GameObject[flockPopulation];

        for(int i = 0; i < flockPopulation; i++)
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x -flockRange / 4, transform.position.x + flockRange / 4),
                                        Random.Range(transform.position.y, transform.position.y + flockRange / 4),
                                        Random.Range(transform.position.z - flockRange / 4, transform.position.z + flockRange / 4));

            flockAgents[i] = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length - 1)], pos, new Quaternion(transform.rotation.x, Random.Range(0, 360), transform.rotation.z, 0));
            flockAgents[i].GetComponent<flockBehaviour>().controller = this;
        }

        totalAgents = flockAgents.Concat(values.otherAI).ToArray();

        goalPos = new Vector3(Random.Range(-flockRange, flockRange),
                        transform.position.y,
                            Random.Range(-flockRange, flockRange));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (updateTimer <= 0 || Vector3.Distance(flockAgents[0].transform.position, goalPos) < 1)
        {
            goalPos = new Vector3(Random.Range(-flockRange, flockRange),
                        transform.position.y,
                            Random.Range(-flockRange, flockRange));
            updateTimer = 5.0f;
        }

        updateTimer -= Time.deltaTime;
	}
}

