using UnityEngine;

public class weaponScript : MonoBehaviour
{
   
    public float projectileRange;
    public int ammo;
    public float scaleLimit;
    bool enemyInSight = false;
    public LayerMask targetLayer;
    public GameObject parent;

    AudioSource gunshot;

    float rateOfFire = 0;
    // Use this for initialization
    void Start ()
    {
        gunshot = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rateOfFire -= Time.deltaTime;
        Debug.DrawRay(parent.transform.position, parent.transform.forward * projectileRange, Color.red);

        RaycastHit hitTarget;
        if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hitTarget, targetLayer))
        {
            enemyInSight = hitTarget.collider ? true : false;
        }
	}

    public bool enemyInSights()
    {
        return enemyInSight;
    }

    public bool Fire()
    {
        if (rateOfFire <= 0)
        {
            Vector3 direction = Random.insideUnitCircle * scaleLimit;
            RaycastHit hitTarget;
            gunshot.Play();
            rateOfFire = 2.0f;
            if (Physics.Raycast(parent.transform.position, parent.transform.forward + direction, out hitTarget, targetLayer))
            {
                if (hitTarget.transform.tag == "entity" && hitTarget.collider.name != transform.parent.name)
                {
                    GameObject go = hitTarget.transform.gameObject;
                    Debug.Log(hitTarget.collider.name);
                    //Debug.Log("Hit");

                    go.SendMessage("HitByShot");
                }
            }
        }
        return true;
        
    }
}
