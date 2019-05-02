
using UnityEngine;

public class spawnMonster : MonoBehaviour {

    public delegate void workersRun();
    public static event workersRun run;
    public monsterFSMAI monster;

    private void Start()
    {
        monster.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKey(KeyCode.Space) && monster.gameObject.activeSelf == false)
        {
            monster.transform.position = transform.position;

            monster.setTarget(monster.targets[Random.Range(0, monster.targets.Length)].gameObject);
            monster.gameObject.SetActive(true);
        }
	}
}
