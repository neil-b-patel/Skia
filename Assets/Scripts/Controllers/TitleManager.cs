using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(sceneBehavior.StartGame(progressManager.CheckProgress));
    }

    public void NewGame()
    {
        playerData.ResetData();
        StartGame();
    }

    public void LoadGame()
    {
        playerData.LoadData();
        StartGame();
    }
}