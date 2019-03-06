using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class flockController : MonoBehaviour
{


    public GameObject tempEnemyPrefab;

    public int maxEnemies;
    static int numOfEnemies;

    public float roaming;
    public static float patrolAreaSize;
    public GameObject[] flockEnemies;
    public GameObject[] otherAI;
    public static GameObject[] totalEnemies;
    public static Vector3 goalPos = Vector3.zero;

    float updateTimer = 10.0f;

	// Use this for initialization
	void Start ()
    {
        patrolAreaSize = roaming;
        numOfEnemies = maxEnemies;
        flockEnemies = new GameObject[numOfEnemies];
        //findPath = GetComponent<Pathfinding>();

        for(int i = 0; i < numOfEnemies; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-patrolAreaSize / 4, patrolAreaSize / 4),
                                        1,
                                        Random.Range(-patrolAreaSize / 4, patrolAreaSize / 4));

            flockEnemies[i] = Instantiate(tempEnemyPrefab, pos, Quaternion.identity);

        }

        totalEnemies = flockEnemies.Concat(otherAI).ToArray();
        goalPos = new Vector3(Random.Range(-patrolAreaSize, patrolAreaSize),
                        Random.Range(-patrolAreaSize, patrolAreaSize),
                            Random.Range(-patrolAreaSize, patrolAreaSize));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (updateTimer <= 0 || Vector3.Distance(flockEnemies[0].transform.position, goalPos) < 1)
        {
            goalPos = new Vector3(Random.Range(-patrolAreaSize, patrolAreaSize),
                        Random.Range(-patrolAreaSize, patrolAreaSize),
                            Random.Range(-patrolAreaSize, patrolAreaSize));
            updateTimer = 10.0f;
        }

        updateTimer -= Time.deltaTime;
        
	}
}
