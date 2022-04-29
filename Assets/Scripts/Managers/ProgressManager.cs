using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    FadeZoomEffect fadeZoomEffect;
    LightPlayerEvolution lightPlayerEvolution;

    public SceneBehavior sceneBehavior;
    public AudioManager audioManager;
    public PlayerData playerData;
 

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

    public void SetMusic(bool isLightPlayer = true)
    {
        //audioManager = FindObjectOfType<AudioManager>();

        audioManager.StopAll();

        if (isLightPlayer)
        {
            switch (playerData.numFeet)
            {
                case 0:
                    audioManager.Play("Arabian");
                    break;
                case 1:
                    audioManager.Play("Journey");
                    break;
                case 2:
                    audioManager.Play("TitleScreen");
                    break;
            }
        }
        else
        {
            switch (playerData.numFeet)
            {
                case 0:
                    audioManager.Play("Spooky");
                    break;
                case 1:
                    audioManager.Play("Spooky");
                    break;
                case 2:
                    audioManager.Play("Dark");
                    break;
            }
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

    public void GameWin()
    {
        //sceneBehavior = FindObjectOfType<SceneBehavior>();
        sceneBehavior.GameWin();
    }
}
