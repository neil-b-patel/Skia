using UnityEngine;

public class TitleManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;
    PlayerData playerData;

    void Start()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
        playerData = FindObjectOfType<PlayerData>();
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
        sceneBehavior.StartGame(startBtnPressed: true);
    }

    public void NewGame()
    {
        Debug.Log("NEW GAME!");
        playerData.resetData();
    }

    public void LoadGame()
    {
        Debug.Log("LOAD GAME!"); 
        SaveManager.LoadGame();
        //if (data != null)
        //{
            //playerData.loadData(data);
        //}
    }
}