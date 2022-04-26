using UnityEngine;

public class ShadowCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Light Player")
        {
            bool direction;
            direction = other.GetComponent<LightPlayerController>().GetDirectionToFace();
            other.GetComponent<LightPlayerController>().enabled = false;
            other.GetComponent<ShadowPlayerController>().enabled = true;
            other.GetComponent<ShadowPlayerController>().SetDirectionToFace(direction);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Light Player")
        {
            bool direction;
            direction = other.GetComponent<ShadowPlayerController>().GetDirectionToFace();
            other.GetComponent<ShadowPlayerController>().enabled = false;
            other.GetComponent<LightPlayerController>().enabled = true;
            other.GetComponent<LightPlayerController>().SetDirectionToFace(direction);
        }
    }
}
