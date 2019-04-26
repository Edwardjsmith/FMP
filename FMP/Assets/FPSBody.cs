
using UnityEngine;

public class FPSBody : gameEntity
{
    public float movementSpeed = 10;
    CharacterController controller;

    float openDistance = 5.0f;

    public enum type { FPS, Horror, Other};
    public type Type;

    public LayerMask rayLayer;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
        float vertInput = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        controller.SimpleMove(forwardMovement + rightMovement);

        if (Type == type.FPS)
        {
            getAnim().SetFloat("velX", horizInput);
            getAnim().SetFloat("velY", vertInput);
            FPSInput();
        }
        else if(Type == type.Horror)
        {
            horrorInput();
        }
    }

    void horrorInput()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.GetChild(0).transform.position, transform.GetChild(0).transform.forward * openDistance, out hit, rayLayer))
        {
            if(hit.collider.tag == "door")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.SendMessage("OpenDoor");
                }
            }
        }
    }
    void FPSInput()
    {
        if (GetWeapon() != null)
        {
            if (Input.GetMouseButton(0) && GetWeapon().ammo > 0 && !GetWeapon().reloading)
            {
                getAnim().SetBool("transitionToShooting", true);
                GetWeapon().Fire();
            }
            else if (GetWeapon().ammo <= 0)
            {
                GetWeapon().reload(this);
            }
        }
    }
}
