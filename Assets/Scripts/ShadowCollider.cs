using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCollider : MonoBehaviour
{
    GameObject varGameObject = GameObject.Find("Light Player");

    // 1
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Light Player")
        {
            other.GetComponent<ShadowBehavior>().enabled = true;
            other.GetComponent<LightPlayerController>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Light Player")
        {
            other.GetComponent<ShadowBehavior>().enabled = false;
            other.GetComponent<LightPlayerController>().enabled = true;
        }
    }
}
