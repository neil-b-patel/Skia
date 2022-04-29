using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FadeZoomEffect : MonoBehaviour
{
    int startCamZoom = 18;
    int endCamZoom = 10;
    float fadeSpeed = 0.05f;
    float lensAdjust = 0.75f;
    bool fadeToBlack = true;

    public CinemachineVirtualCamera vcam;
    public Animator animator;
    public Rigidbody rb;


    void Start()
    {
        //vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        //rb = GameObject.Find("Light Player").GetComponent<Rigidbody>();
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
        
        animator.enabled = false;
        if (fadeToBlack)
        {
            while (objectColor.a > 0 || vcam.m_Lens.OrthographicSize > endCamZoom)
            {
                if(objectColor.a < 80) {
                    fadeSpeed = 0.15f; 
                }

                if(objectColor.a < 100) {
                    lensAdjust = 1.25f;
                }

                if (objectColor.a > 0)
                {
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    GetComponent<Image>().color = objectColor;
                }

                if (vcam.m_Lens.OrthographicSize > endCamZoom) 
                {
                    vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - (lensAdjust * Time.deltaTime);
                }

                yield return null;
            }
            
            Debug.Log("FADING DONE");
            
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY |
                             RigidbodyConstraints.FreezeRotationZ;
            animator.enabled = true;
            yield break;
        }
    }
}