using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Lives")]
    public Transform livesHolder;
    [Header("Score")]
    public TMP_Text scoreText;
    public int score;

    [Header("Pause")]
    public CanvasGroup pauseCg;
    public TMP_Text pauseTitleText, pauseResumeText;
    public Button resumeButton;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (GameManager.instance.currentState)
            {
               
                case GameState.INGAME:
                case GameState.PAUSE:
                    TogglePause();
                    break;
                case GameState.MAINMENU:
                case GameState.GAMEOVER:
                    break;
            }
        }
    }

    public void LoseLife()
    {
        livesHolder.GetChild(livesHolder.childCount - 1)
            .DOScale(0f,0.25f).SetUpdate(true)
            .OnComplete(() => {
                Destroy(livesHolder.
                    GetChild(livesHolder.childCount - 1).gameObject); 
            });
    }

    public void UpdateScoreDisplay() {
        scoreText.text = $"{score.ToString("00000")}";

    }

    public void IncreaseScore(int ss) {
        score += ss;
        UpdateScoreDisplay();
    }

    public void TogglePause() {

        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(TogglePause);
        pauseTitleText.text = "Paused";
        pauseResumeText.text = "Resume";

        pauseCg.interactable = false;
        pauseCg.blocksRaycasts = false;
        GameManager.ChangeState(pauseCg.alpha == 1f ? GameState.INGAME : GameState.PAUSE);
        pauseCg.DOFade(pauseCg.alpha == 1f ? 0f : 1f, 0.2f).SetUpdate(true).OnComplete(() =>
          {
              pauseCg.interactable = pauseCg.alpha == 1f;
              pauseCg.blocksRaycasts = pauseCg.alpha == 1f;

          }
         );
    }

    public void OnPlayAgain() {
        GameManager.LoadScene(GameManager.SCENE_MAIN);
    
    }

    public void OnBackToMenu() {
        GameManager.LoadScene(GameManager.SCENE_MENU);
    }

    public void TriggerGameOver() {
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(OnPlayAgain);
        GameManager.ChangeState(GameState.GAMEOVER);
        pauseTitleText.text = "Game Over";
        pauseResumeText.text = "Play Again?";
        pauseCg.DOFade(pauseCg.alpha == 1f ? 0f : 1f, 0.2f).SetUpdate(true).OnComplete(() =>
        {
            pauseCg.interactable = pauseCg.alpha == 1f;
            pauseCg.blocksRaycasts = pauseCg.alpha == 1f;

        }
        );
    }
    }
