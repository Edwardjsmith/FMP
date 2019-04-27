using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasCollision : MonoBehaviour
{
    public RectTransform position;
    public LayerMask otherUI;
	// Use this for initialization
	void Start ()
    {
        position = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<canvasCollision>())
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider != null)
                {
                    position.position = new Vector2(position.position.x, position.position.y - 50);
                }
            }
        }
    }
}
