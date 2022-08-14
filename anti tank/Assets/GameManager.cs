using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Pause,
    Play,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public Canvas PauseMenu;
    public Canvas GameOverUI;
    public bool gamePaused = false;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance is null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return _instance;
        }
    }

    public GameState State;

    private void Awake()
    {
        _instance = this;
        State = GameState.Play;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Pause:
                Pause();
                break;
            case GameState.Play:
                Play();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            default:
                break;
        }
    }

    private void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        PauseMenu.gameObject.SetActive(true);
        Debug.Log("Game paused");
    }
    private void Play()
    {
        gamePaused = false;
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        GameOverUI.gameObject.SetActive(true);
    }
}
