using UnityEngine;
using UnityEngine.UI;

public class weaponScript : MonoBehaviour
{
    public Transform view;
    public float projectileRange;
    public int ammo;
    public float scaleLimit;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(view.position, view.forward * projectileRange, Color.red);
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
                go.SendMessage("HitByShot");
            }
        }
    }
}
