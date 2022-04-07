using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;
    PlayerData playerData;
    ProgressManager progressManager;

    void Start()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
        playerData = FindObjectOfType<PlayerData>();
        progressManager = FindObjectOfType<ProgressManager>();
    }

    public void UnloadMenu()
    {
        sceneBehavior.UnloadMenu(unpauseBtnPressed: true);
    }

    public void SaveGame()
    {
        playerData.SaveData();
    }

    public void LoadGame()
    {
        playerData.LoadData();
        progressManager.CheckProgress();
    }

    public void QuitGame()
    {
        sceneBehavior.QuitGame();
    }
}
