using UnityEngine;

public class weaponScript : MonoBehaviour
{
   
    public float projectileRange;
    public int ammo;
    public float scaleLimit;
    bool enemyInSight = false;
    public LayerMask targetLayer;
    public GameObject parent;
    Camera parentCam;
    ParticleSystem muzFLash;

    AudioSource gunshot;

    float rateOfFire = 0;
    float reloadTime = 3.0f;
    public bool reloading = false;

    public RectTransform crosshair;

    // Use this for initialization
    void Start ()
    {
        if(parent.GetComponent<Camera>())
        {
            parentCam = parent.GetComponent<Camera>();
        }
        gunshot = GetComponent<AudioSource>();
        muzFLash = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rateOfFire -= Time.deltaTime;
        Debug.DrawRay(parent.transform.position, parent.transform.forward, Color.red);

        RaycastHit hitTarget;
        if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hitTarget))
        {
        if (crosshair != null)
        {
            crosshair.position = parentCam.WorldToScreenPoint(hitTarget.point);
        }
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
            RaycastHit hitTarget;
            gunshot.Play();
            muzFLash.Play();
            rateOfFire = 0.2f;
            ammo--;
            if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hitTarget))
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
