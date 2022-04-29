using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavior : MonoBehaviour
{
    public ProgressManager progressManager;
    public AudioManager audioManager;

    public enum Scenes
    {
        Core,
        Title,
        Level01,
        Platformer,
        Menu,
        GameOver
    }

    bool onTitle = true;
    bool menuLoaded = false;

    void Start()
    {
        audioManager.Play("TitleScreen");

        SceneManager.LoadScene((int)Scenes.Title, LoadSceneMode.Additive);
    }

    void Update()
    {
        LoadMenu();
    }

    void LoadMenu()
    {
        if (!onTitle && Input.GetKeyDown(KeyCode.Escape) && !menuLoaded)
        {
            Time.timeScale = 0;
            
            SceneManager.LoadScene((int)Scenes.Menu, LoadSceneMode.Additive);
           
            menuLoaded = true; 
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
            Time.timeScale = 1;

            SceneManager.UnloadSceneAsync((int)Scenes.Menu);
            
            menuLoaded = false;
        }
    }

    public IEnumerator StartGame(System.Action checkProgressCallback)
    {
        SceneManager.LoadSceneAsync((int)Scenes.Level01, LoadSceneMode.Additive);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)Scenes.Platformer, LoadSceneMode.Additive);

        onTitle = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        checkProgressCallback();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.UnloadSceneAsync((int)Scenes.Level01);
        SceneManager.UnloadSceneAsync((int)Scenes.Platformer);
        SceneManager.LoadSceneAsync((int)Scenes.GameOver, LoadSceneMode.Additive);

        Debug.Log("GAME OVER!");
    }

    public void GameWin()
    {
        Debug.Log("YOU WON!");
    }

    public void StartFromTitle()
    {
        StartCoroutine(StartGame(progressManager.CheckProgress));
        SceneManager.UnloadSceneAsync((int)Scenes.Title);
    }

    public void StartFromGameOver()
    {
        StartCoroutine(StartGame(progressManager.CheckProgress));
        SceneManager.UnloadSceneAsync((int)Scenes.GameOver);
    }
}
