using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{
    float timer = 10.0f;
    
    private void Start()
    {
    }
    public bool TransitionToGuard()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 10.0f;
            return true;
        }
        else
        {
            return false;
        }
    }
}
