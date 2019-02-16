using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTriggers : MonoBehaviour
{
    static float counter = 0;

    public static bool trigger1()
    { 
        if(counter < 10.0f)
        {
            counter += 1;
            return false;
        }
        else
        {
            counter = 0;
            return true;
        }

        
    }

    public static bool trigger2()
    {
        if (counter < 10.0f)
        {
            counter += 1;
            return false;
        }
        else
        {
            counter = 0;
            return true;
        }
    }

    public static bool trigger3()
    {
        if (counter < 10.0f)
        {
            counter += 1;
            return false;
        }
        else
        {
            counter = 0;
            return true;
        }
    }


}
