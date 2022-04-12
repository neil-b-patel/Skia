using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCollider : MonoBehaviour
{
    GameObject varGameObject = GameObject.Find("Light Player");

    // 1
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TEST");
        //2
        if (other.name == "Light Player")
        {
            
            Debug.Log("ENTERED");
            other.GetComponent<ShadowBehavior>().enabled = true;
            other.GetComponent<LightPlayerController>().enabled = false;
        }
    }
    // 3
    void OnTriggerExit(Collider other)
    {
        // 4
        if (other.name == "Light Player")
        {
            Debug.Log("EXITED");
            other.GetComponent<ShadowBehavior>().enabled = false;
            other.GetComponent<LightPlayerController>().enabled = true;
        }
    }
}
