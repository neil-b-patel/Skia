using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FadeZoomEffect : MonoBehaviour
{
    bool fadeToBlack = true;
    float fadeSpeed = 0.35f;
    float lensAdjust = 0.35f;

    private CinemachineVirtualCamera vcam;
    private Rigidbody rb;

    void Start()
    {
        vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        rb = GameObject.Find("Light Player").GetComponent<Rigidbody>();
    }

    void Update()
    {   
        // plan on changing the key dow to mouse click or etner for starting the game
            
    }

    //notes: have camera follow turned off at start, but when player presses play, it will turn on the follow machine to create the zoom affect

    public void PlayerWakeUp()
    {
        StartCoroutine(FadeAndZoom());
    }

    public IEnumerator FadeAndZoom()
    {

        Color objectColor = GetComponent<Image>().color;
        float fadeAmount;
        
        rb.constraints = RigidbodyConstraints.FreezePositionX | 
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ;
        
        if (fadeToBlack)
        {
            while (GetComponent<Image>().color.a > 0)
            {
                Debug.Log("FADING FROM BLACK!");
                
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
               
                if (vcam.m_Lens.OrthographicSize > 5) 
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
                             RigidbodyConstraints.FreezeRotationZ;


            yield break;
        }
    }
}