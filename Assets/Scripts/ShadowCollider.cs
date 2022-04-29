using UnityEngine;

// ADD TO LIGHT/SHADOW PLAYERS INSTEAD OF EACH LIGHT
// TAG LIGHT COLLIDERS AS "Light"
// ON ENTER TRIGGER (TAG == LIGHT) => LIGHT PLAYER -> SHADOW PLAYER
// ON EXIT TRIGGER (TAG == LIGHT) => SHADOW PLAYER -> LIGHT PLAYER
public class ShadowCollider : MonoBehaviour
{


    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "Light Player")
    //    {
    //        bool direction;
    //        direction = other.GetComponent<LightPlayerController>().GetDirectionToFace();
    //        other.GetComponent<LightPlayerController>().enabled = false;
    //        // DISABLE ENTIRE LIGHT PLAYER GAME OBJECT
    //        // ENABLE ENTIRE SHADOW PLAYER GAME OBJECT
    //            // ADD SPRITE + ANIMATOR TO SHADOW PLAYER
    //        other.GetComponent<ShadowPlayerController>().enabled = true;
    //        other.GetComponent<ShadowPlayerController>().SetDirectionToFace(direction);
    //        // other.GetComponent<SpriteRenderer>().sprite = "";
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.name == "Light Player")   // CHANGE TO "Shadow Player"
    //    {
    //        bool direction;
    //        direction = other.GetComponent<ShadowPlayerController>().GetDirectionToFace();
    //        other.GetComponent<ShadowPlayerController>().enabled = false;
    //        other.GetComponent<LightPlayerController>().enabled = true;
    //        other.GetComponent<LightPlayerController>().SetDirectionToFace(direction);
    //    }
    //}
}
