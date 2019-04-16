
using UnityEngine;

public class gameEntity : MonoBehaviour
{
    protected float speed;
    protected float health;
    weaponScript weapon;

    // Use this for initialization
    public virtual void Start ()
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

    public virtual void HitByShot(float damage)
    {
        health -= damage;
    }

    public weaponScript GetWeapon()
    {
        return weapon;
    }
}
