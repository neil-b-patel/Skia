using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavior : MonoBehaviour
{
    public enum Scenes
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
        FindObjectOfType<AudioManager>().Play("TitleScreen");

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
        SceneManager.UnloadSceneAsync((int)Scenes.Title);

        FindObjectOfType<AudioManager>().Stop("TitleScreen");

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

    //public Scene[] GetScenes()
    //{
    //    Scene[] scenes = new Scene[SceneManager.sceneCount]; 

    //    for (int i = 0;  i < SceneManager.sceneCount; i++)
    //    {
    //        scenes[i] = SceneManager.GetSceneAt(i);
    //    }

    //    return scenes; 
    //}
}
