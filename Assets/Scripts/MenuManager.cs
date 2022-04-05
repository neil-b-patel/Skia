using UnityEngine;

public class MenuManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;
    PlayerData playerData;

    void Start()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
        playerData = FindObjectOfType<PlayerData>();
    }

    public void UnloadMenu()
    {
        sceneBehavior.UnloadMenu(unpauseBtnPressed: true);
    }

    public void SaveGame()
    {
        SaveManager.SaveGame(playerData);
    }

    public void LoadGame()
    {
        PlayerData data = SaveManager.LoadGame();
        playerData.loadData(data);
    }

    public void QuitGame()
    {
        sceneBehavior.QuitGame();
    }
}
