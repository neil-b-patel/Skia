using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FadeZoomEffect : MonoBehaviour
{
    int startCamZoom = 13;
    float fadeSpeed = 0.01f;
    float lensAdjust = 0.35f;
    bool fadeToBlack = true;

    private CinemachineVirtualCamera vcam;
    private Rigidbody rb;

    void Start()
    {
        vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        rb = GameObject.Find("Light Player").GetComponent<Rigidbody>();
    }

    //notes: have camera follow turned off at start,
    //       but when player presses play,
    //       it will turn on the follow machine to create the zoom effect

    public void PlayerWakeUp()
    {
        StartCoroutine(FadeAndZoom());
    }

    public IEnumerator FadeAndZoom()
    {

        Color objectColor = GetComponent<Image>().color;
        float fadeAmount;

        vcam.m_Lens.OrthographicSize = startCamZoom;
        
        rb.constraints = RigidbodyConstraints.FreezePositionX | 
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
        
        if (fadeToBlack)
        {
            while (objectColor.a > 0)
            {
                if(objectColor.a < 80) {
                    fadeSpeed = 0.10f; 
                }

                if(objectColor.a < 100) {
                    lensAdjust = .80f;
                }

                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                if (vcam.m_Lens.OrthographicSize > 6) 
                {
                    vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - (lensAdjust * Time.deltaTime);
                }
                
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                
                GetComponent<Image>().color = objectColor;

                yield return null;
            }
            
            Debug.Log("FADING DONE");
            
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY |
                             RigidbodyConstraints.FreezeRotationZ;

            yield break;
        }
    }
}