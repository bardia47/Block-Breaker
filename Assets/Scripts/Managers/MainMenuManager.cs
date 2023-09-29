using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame() {
        GameManager.LoadScene(GameManager.SCENE_MAIN);
    }

    public void OnQuitGame() {
        Application.Quit();
    }
}
