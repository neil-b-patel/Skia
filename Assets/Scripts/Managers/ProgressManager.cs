using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;
    PlayerData playerData;
    AudioManager audioManager;
    FadeZoomEffect fadeZoomEffect;
    LightPlayerEvolution lightPlayerEvolution;

    void Start()
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
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.StopAll();

        switch(playerData.items.Count)
        {
            case 0:
                //audioManager.Stop("TitleScreen", fade: true);
                audioManager.Play("Dark");
                break;
            case 1:
                //audioManager.Stop("Dark");
                audioManager.Play("Spooky");
                break;
            case 2:
                //audioManager.Stop("Spooky");
                audioManager.Play("Arabian");
                break;
            case 3:
                //audioManager.Stop("Arabian");
                audioManager.Play("Journey");
                break;
            case 4:
                //audioManager.Stop("Journey");
                audioManager.Play("TitleScreen");
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

    public void GameWin()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
        sceneBehavior.GameWin();
    }
}
