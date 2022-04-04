using UnityEngine;

public class MenuManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;

    void Start()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
    }

    void SaveGame()
    {
        Debug.Log("SAVE GAME!");
        // ADD CODE TO SAVE TO PREFS
    }

    void LoadGame()
    {
        Debug.Log("LOAD GAME");
        // ADD CODE TO LOAD FROM PREFS
    }

    public void UnloadMenu()
    {
        sceneBehavior.UnloadMenu(unpauseBtnPressed: true);
    }

    public void QuitGame()
    {
        sceneBehavior.QuitGame();
    }
}
