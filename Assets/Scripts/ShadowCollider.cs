using UnityEngine;

public class ShadowCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Light Player")
        {
            other.GetComponent<ShadowPlayerController>().enabled = true;
            other.GetComponent<LightPlayerController>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Light Player")
        {
            other.GetComponent<ShadowPlayerController>().enabled = false;
            other.GetComponent<LightPlayerController>().enabled = true;
        }
    }
}
