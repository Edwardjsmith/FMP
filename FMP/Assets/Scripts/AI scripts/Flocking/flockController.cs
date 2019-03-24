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
    public Vector3 targetPos = Vector3.zero;

    public GameObject[] waypoints;

    float updateTimer = 10.0f;
    float seperationDistance;

    public Vector3 averagePos = Vector3.zero;
    public Vector3 seperation = Vector3.zero;
    public Vector3 averageHeading = Vector3.zero;
    public float averageSpeed = 0.1f;

    Camera cam;

    // Use this for initialization
    void Start ()
    {
        values = FindObjectOfType<setControllerValues>();
        flockRange = values.flockRange;
        flockPopulation = values.flockPopulation;

        flockAgents = new GameObject[flockPopulation];

        for(int i = 0; i < flockPopulation; i++)
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x - flockRange / 4, transform.position.x + flockRange / 4),
                                        Random.Range(5, 15),
                                        Random.Range(transform.position.z - flockRange / 4, transform.position.z + flockRange / 4));

            flockAgents[i] = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length - 1)], pos, new Quaternion(transform.rotation.x, Random.Range(0, 360), transform.rotation.z, 0));
            flockAgents[i].GetComponent<flockBehaviour>().controller = this;
        }

        totalAgents = flockAgents.Concat(values.otherAI).ToArray();

        targetPos = new Vector3(Random.Range(-flockRange, flockRange),
                        Random.Range(-flockRange, flockRange),
                            Random.Range(-flockRange, flockRange));
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (updateTimer <= 0 || Vector3.Distance(transform.position, targetPos) < 1)
        {
            targetPos = new Vector3(Random.Range(-flockRange, flockRange),
                        Random.Range(-flockRange, flockRange),
                            Random.Range(-flockRange, flockRange));
            updateTimer = 10.0f;
        }

        transform.position = flockAgents[0].transform.position;
        flockAgents[0].GetComponent<Renderer>().enabled = false;
        updateTimer -= Time.deltaTime;
	}

    private void FixedUpdate()
    {
        GameObject lastObj = null;
        averageSpeed = 0;
        averagePos = Vector3.zero;
        foreach (GameObject go in flockAgents)
        {
            averagePos = (averagePos + go.transform.position) + (new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1) * Random.Range(1, 6));
            averageSpeed = averageSpeed + go.GetComponent<flockBehaviour>().speed;
            if (lastObj != null)
            {
                seperation = seperation + (lastObj.transform.position - go.transform.position);
            }
            lastObj = go;
        }

        
        averagePos = averagePos / (flockAgents.Length - 1);
        averageSpeed = averageSpeed / (flockAgents.Length - 1);
        seperation = seperation / (flockAgents.Length - 1);
    }
}

