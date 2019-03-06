using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEntity : MonoBehaviour
{
    public float speed;
    public float health;
    weaponScript weapon;
    // Use this for initialization
    void Start ()
    {
        if (GetComponentInChildren<weaponScript>())
        {
            weapon = GetComponentInChildren<weaponScript>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void HitByShot(float damage)
    {
        health -= damage;
    }

    public weaponScript GetWeapon()
    {
        return weapon;
    }
}
