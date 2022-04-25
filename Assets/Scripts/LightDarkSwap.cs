using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDarkSwap : MonoBehaviour
{   
    ShadowCollider shadowCollider; 
    GameObject lightPlayer;
    GameObject shadowPlayer;

    // Start is called before the first frame update
    void Start()
    {
        shadowCollider = FindObjectOfType<ShadowCollider>();
        lightPlayer = this.transform.Find("Light Player").gameObject;
        shadowPlayer = this.transform.Find("Shadow Player").gameObject;
    }
    void Update() 
    {
        shadowCheck();
        normalizePosition();
    }
    void shadowCheck()
    {   
        if(shadowCollider.inShadow == true) {
            shadowPlayer.SetActive(true);
            shadowPlayer.GetComponent<ShadowBehavior>().enabled = true;
            lightPlayer.SetActive(false);
        }

        if(shadowCollider.inShadow == false) {
            shadowPlayer.SetActive(false);
            lightPlayer.SetActive(true);
        }
    }

    void normalizePosition() {
        if(shadowPlayer.activeSelf == false) {
            shadowPlayer.transform.position = lightPlayer.transform.position;
        }
        if(lightPlayer.activeSelf == false) {
            lightPlayer.transform.position = shadowPlayer.transform.position;
        }
    }

}
