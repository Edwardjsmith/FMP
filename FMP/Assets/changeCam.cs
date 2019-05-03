using System.Collections.Generic;
using UnityEngine;

public class changeCam : MonoBehaviour
{
    public List<Camera> cams;
    int currentIndex = 0;

    Camera currentCamera;
    // Use this for initialization
    private void Start()
    {
        foreach(Camera cam in cams)
        {
            cam.gameObject.SetActive(false);
        }

        currentCamera = cams[0];
        currentCamera.gameObject.SetActive(true);

    }

    private bool inBounds(int index, List<Camera> List)
    {
        return (index >= 0) && (index < List.Count);
    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(currentCamera.transform.parent != null)
            {
                currentCamera.transform.parent.gameObject.SetActive(false);
                currentCamera.gameObject.SetActive(false);
            }
            else
            {
                currentCamera.gameObject.SetActive(false);
            }
            if(inBounds(currentIndex + 1, cams))
            {
                currentIndex += 1;
            }
            else
            {
                currentIndex = 0;
            }

            currentCamera = cams[currentIndex];

            if (currentCamera.transform.parent != null)
            {
                currentCamera.transform.parent.gameObject.SetActive(true);
                currentCamera.gameObject.SetActive(true);
            }
            else
            {
                currentCamera.gameObject.SetActive(true);
            }
        }

        



    }
}
