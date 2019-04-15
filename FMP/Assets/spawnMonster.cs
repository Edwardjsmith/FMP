using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMonster : MonoBehaviour {

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
            foreach(stateMachineAI guard in monster.targets)
            {
                guard.target = monster.gameObject;
            }
            monster.target = monster.targets[Random.Range(0, 1)].gameObject;
            monster.gameObject.SetActive(true);
        }
	}
}
