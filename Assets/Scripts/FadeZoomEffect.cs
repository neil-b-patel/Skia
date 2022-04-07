using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FadeZoomEffect : MonoBehaviour
{
    public GameObject blackOutSquare;
    public CinemachineVirtualCamera vcam;
    public GameObject player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(FadeAndZoom());
    }

    // Update is called once per frame
    void Update()
    {   
        // plan on changing the key dow to mouse click or etner for starting the game
            
    }

    //notes: have camera follow turned off at start, but when player presses play, it will turn on the follow machine to create the zoom affect

    public IEnumerator FadeAndZoom(bool fadeToBlack = true, float fadeSpeed = .3f, float lensAdjust = 1f)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;
        rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |RigidbodyConstraints.FreezePositionZ;
        if(fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                Debug.Log("fading!!");
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                if(vcam.m_Lens.OrthographicSize > 5) {
                    vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - (lensAdjust * Time.deltaTime);
                }
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            Debug.Log("Done");
            rb.constraints = RigidbodyConstraints.None;
            yield break;
        }
    }
}