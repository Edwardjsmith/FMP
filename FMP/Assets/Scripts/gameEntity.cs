
using UnityEngine;

public class gameEntity : MonoBehaviour
{
    protected float speed;
    protected float health;
    weaponScript weapon;

    Animator anim;
    // Use this for initialization
    public virtual void Start ()
    {
        if (GetComponentInChildren<weaponScript>())
        {
            weapon = GetComponentInChildren<weaponScript>();
        }

        if (GetComponentInChildren<Animator>())
        {
            anim = GetComponentInChildren<Animator>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public weaponScript GetWeapon()
    {
        return weapon;
    }

    public Animator getAnim()
    {
        return anim;
    }
}
