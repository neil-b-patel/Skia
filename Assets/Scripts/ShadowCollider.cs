using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCollider : MonoBehaviour
{   
    public bool inShadow = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Light Player")
        {
            // other.GetComponent<ShadowBehavior>().enabled = true;
            // other.GetComponent<LightPlayerController>().enabled = false;
            inShadow = true;
        }
        inShadow = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Shadow Player")
        {
            // other.GetComponent<ShadowBehavior>().enabled = false;
            // other.GetComponent<LightPlayerController>().enabled = true;
            inShadow = false;
        }
        inShadow = false;
    }
}
