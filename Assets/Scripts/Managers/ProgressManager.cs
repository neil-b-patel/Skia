using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    private PlayerData playerData;
    private FadeZoomEffect fadeZoomEffect;
    private LightPlayerEvolution lightPlayerEvolution;

    void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    public void CheckProgress()
    {
        fadeZoomEffect = FindObjectOfType<FadeZoomEffect>();
        lightPlayerEvolution = FindObjectOfType<LightPlayerEvolution>();

        SetMusic();
        WakeUpPlayer();
        EvolvePlayer();

        if (playerData.items.Count > 0)
        {
            Debug.Log("DESTROYING REDUNDANT ITEMS");
            DestroyRedundantItems();
        }
    }

    public void SetMusic()
    {
        switch(playerData.items.Count)
        {
            case 0:
                //FindObjectOfType<AudioManager>().Stop("TitleScreen", fade: true);
                FindObjectOfType<AudioManager>().Play("Dark");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Stop("Dark");
                FindObjectOfType<AudioManager>().Play("Spooky");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Stop("Spooky");
                FindObjectOfType<AudioManager>().Play("Arabian");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Stop("Arabian");
                FindObjectOfType<AudioManager>().Play("Journey");
                break;
            case 4:
                FindObjectOfType<AudioManager>().Stop("Journey");
                FindObjectOfType<AudioManager>().Play("TitleScreen");
                break;
        }
    }

    void DestroyRedundantItems()
    {
        GameObject[] sceneItems = GameObject.FindGameObjectsWithTag("CanBePickedUp");

        foreach (GameObject sceneItem in sceneItems)
        {
            if (playerData.items.Contains(sceneItem.name))
            {
                Destroy(sceneItem);
            }
        }
    }

    void WakeUpPlayer()
    {
        try
        {
            if (playerData.items.Count == 0)
            {
                fadeZoomEffect.PlayerWakeUp();
            }
            else
            {
                fadeZoomEffect.gameObject.SetActive(false);
            }
        }
        catch
        {
            Debug.LogWarning("COULD NOT WAKE UP PLAYER");
        }
            
    }

    public void EvolvePlayer()
    {
        lightPlayerEvolution.EvolvePlayer();
    }
}
