using UnityEngine;

public class weaponScript : MonoBehaviour
{
   
    public float projectileRange;
    public int ammo;
    public float scaleLimit;
    bool enemyInSight = false;
    public LayerMask targetLayer;
    public GameObject parent;
    ParticleSystem muzFLash;

    AudioSource gunshot;

    float rateOfFire = 0;
    float reloadTime = 3.0f;
    public bool reloading = false;
    public LineRenderer laser;

    // Use this for initialization
    void Start ()
    {
        gunshot = GetComponent<AudioSource>();
        muzFLash = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        rateOfFire -= Time.deltaTime;
        

        RaycastHit hitTarget;
        if (Physics.Raycast(parent.transform.position, parent.transform.TransformDirection(Vector3.forward), out hitTarget))
        {
            laser.SetPosition(0, parent.transform.position);
            laser.SetPosition(1, parent.transform.TransformDirection(Vector3.forward) * hitTarget.distance);
            Debug.DrawRay(parent.transform.position, parent.transform.TransformDirection(Vector3.forward) * hitTarget.distance, Color.red);
            enemyInSight = hitTarget.collider.name == "Player" ? true : false;
        }
	}

    public bool enemyInSights()
    {
        return enemyInSight;
    }

    public void reload(gameEntity bot)
    {
        reloadTime -= Time.deltaTime;
        reloading = true;
        if (reloadTime <= 0)
        {
            reloadTime = 3.0f;
            ammo = 10;
            reloading = false;
        }
    }

    public bool Fire()
    {
        if (rateOfFire <= 0)
        {
            Vector3 direction = Random.insideUnitCircle * scaleLimit;
            RaycastHit hitTarget;
            gunshot.Play();
            muzFLash.Play();
            rateOfFire = 0.2f;
            ammo--;
            if (Physics.Raycast(parent.transform.position, parent.transform.forward + direction, out hitTarget))
            {
                if(hitTarget.collider.name != transform.parent.name)
                {
                    GameObject go = hitTarget.transform.gameObject;
                    Debug.Log(hitTarget.collider.name);

                    if (hitTarget.collider.tag == "entity")
                    {
                        go.SendMessage("HitByShot");
                    }
                }
            }
        }
        return true;
    }
}
