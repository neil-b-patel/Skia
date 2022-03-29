//using System.Collections;
//using System.Collections.Generic;
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
        if (onTitle && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene((int) Scenes.Level01, LoadSceneMode.Additive);
            SceneManager.LoadScene((int) Scenes.Platformer, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync((int) Scenes.Title);

            onTitle = false;
        }
        if (!onTitle && Input.GetKeyDown(KeyCode.Escape) && !menuLoaded)
        {
            SceneManager.LoadScene((int) Scenes.Menu, LoadSceneMode.Additive);
            menuLoaded = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuLoaded)
        {
            SceneManager.UnloadSceneAsync((int) Scenes.Menu);
            menuLoaded = false;
        }
    }
}
