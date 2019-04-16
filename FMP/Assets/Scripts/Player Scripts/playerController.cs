using UnityEngine;

public class playerController : gameEntity
{
    private CharacterController Controller;
    Camera view;

    public float Sensitivity;
    public float moveSpeed;
    float xAxisClamp = 0.0f;

    Animator anim;

    public override void Start()
    {
        base.Start();
        if(GetComponentInChildren<Animator>())
        {
            anim = GetComponentInChildren<Animator>();
        }
    }
    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        view = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        rotateView();
        movement();

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        Application.Quit();
    }
    private void Update()
    {
        

        if(Input.GetMouseButton(0))
        {
            if (GetWeapon() != null)
            {
                anim.Play("aimIdle");
                GetWeapon().Fire();
                Debug.Log("Fire");
            }
        }
    }

    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if(vertical > 0)
        {
            anim.Play("Run");
        }
        else
        {
            anim.Play("aimIdle");
        }

        //Movement

        //Move side to side
        Vector3 moveHorizontal = transform.right * horizontal;

        //Move forward and back
        Vector3 moveForward = transform.forward * vertical;

        //Implement said moves
        Controller.SimpleMove(moveHorizontal);
        Controller.SimpleMove(moveForward);
        
    }

    void rotateView()
    {
        float rotX = -Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float rotY = -Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xAxisClamp -= rotY;

        // Clamps the camera in the x between -90 and 90 (directly up and directly down)
        //To stop flicker
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rotY = 0;
            CLampXAxisRotationToValue(270);
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotY = 0;
            CLampXAxisRotationToValue(90);
        }

        view.transform.Rotate(-Vector3.right * rotY);
        transform.Rotate(Vector3.up * rotX);

    }

    void CLampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = view.transform.eulerAngles;
        eulerRotation.x = value;
        view.transform.eulerAngles = eulerRotation;
    }
}
