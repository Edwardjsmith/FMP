using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : MonoBehaviour
{

    private static AlertState instance = null;

    public static AlertState Instance()
    {
        if (instance == null)
        {
            instance = new AlertState();
        }
        return instance;
    }


    // Use this for initialization
    void Start ()
    {
        instance = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
