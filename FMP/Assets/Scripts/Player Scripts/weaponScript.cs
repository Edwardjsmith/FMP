using UnityEngine;
using UnityEngine.UI;

public class weaponScript : MonoBehaviour
{
   
    public float projectileRange;
    public int ammo;
    public float scaleLimit;

    bool enemyInSight = false;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.parent.position, transform.parent.forward * projectileRange, Color.red);

        RaycastHit hitTarget;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hitTarget))
        {
            enemyInSight = hitTarget.transform.tag == "entity" ? true : false;
        }
	}

    public bool enemyInSights()
    {
        return enemyInSight;
    }

    public void Fire()
    {
        Vector3 direction = Random.insideUnitCircle * scaleLimit;
        RaycastHit hitTarget;
        if (Physics.Raycast(transform.position, transform.forward + direction, out hitTarget))
        {
            if(hitTarget.transform.tag == "Entity")
            {
                GameObject go = hitTarget.transform.gameObject;
                go.SendMessage("HitByShot", 1);
            }
        }
    }
}
