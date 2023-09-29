using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MAINMENU,
    INGAME,
    PAUSE,
    GAMEOVER
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentState;


    public const string SCENE_MAIN = "Main";
    public const string SCENE_MENU = "Menu";
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == SCENE_MAIN)
        {
            ChangeState(GameState.INGAME);
        }
        if (scene.name == SCENE_MENU)
        {
            ChangeState(GameState.MAINMENU);
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public static void ChangeState(GameState newState) {
        if (instance.currentState == newState)
            return;

        instance.currentState = newState;
        switch (newState)
        {
            case GameState.MAINMENU:
            case GameState.INGAME:
                Time.timeScale = 1f;
                break;
            case GameState.PAUSE:
            case GameState.GAMEOVER:
                Time.timeScale = 0f;
                break;
        }
    }

    public static void LoadScene(string sceneName) {

        SceneManager.LoadScene(sceneName);
    }
}
