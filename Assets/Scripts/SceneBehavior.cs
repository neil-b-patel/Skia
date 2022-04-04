using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavior : MonoBehaviour
{
    enum Scenes
    {
        Core,
        Title,
        Level01,
        Platformer,
        Menu
    }

    private bool onTitle = true;
    private bool menuLoaded = false;

    void Start()
    {
        SceneManager.LoadScene((int) Scenes.Title, LoadSceneMode.Additive);
    }

    void Update()
    {
        LoadMenu();
    }

    void LoadMenu()
    {
        if (!onTitle && Input.GetKeyDown(KeyCode.Escape) && !menuLoaded)
        {
            SceneManager.LoadScene((int)Scenes.Menu, LoadSceneMode.Additive);
            menuLoaded = true; 

            Debug.Log("LOAD MENU!");
        }

        else
        {
            UnloadMenu();
        }
    }

    public void UnloadMenu(bool unpauseBtnPressed = false)
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || unpauseBtnPressed) && menuLoaded)
        {
            SceneManager.UnloadSceneAsync((int)Scenes.Menu);
            menuLoaded = false;

            Debug.Log("UNLOAD MENU!");
        }
    }

    public void StartGame(bool startBtnPressed = false)
    {
        if (startBtnPressed)
        {
            SceneManager.LoadScene((int)Scenes.Level01, LoadSceneMode.Additive);
            SceneManager.LoadScene((int)Scenes.Platformer, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync((int)Scenes.Title);

            onTitle = false;

            Debug.Log("START GAME!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }
}
