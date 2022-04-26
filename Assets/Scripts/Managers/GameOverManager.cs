using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;
    PlayerData playerData;
    ProgressManager progressManager;

    void Start()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
        playerData = FindObjectOfType<PlayerData>();
    }

    public void NewGame()
    {
        playerData.ResetData();
        StartGame();
    }

    public void StartGame()
    {
        sceneBehavior.StartFromGameOver();
    }

    public void LoadGame()
    {
        playerData.LoadData();
        StartGame();
    }

    public void QuitGame()
    {
        sceneBehavior.QuitGame();
    }
}
