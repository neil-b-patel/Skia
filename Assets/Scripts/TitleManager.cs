using UnityEngine;

public class TitleManager : MonoBehaviour
{
    SceneBehavior sceneBehavior;

    void Start()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();

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
}