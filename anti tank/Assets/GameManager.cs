using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public TextMeshProUGUI ScoreUI;

    public float score;

    public bool gameOver = false;
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
    private void Update()
    {
        ScoreUI.text = "Score: " + score.ToString();
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
        if (!gameOver)
        {
            gamePaused = true;
            Time.timeScale = 0f;
            PauseMenu.gameObject.SetActive(true);
            Debug.Log("Game paused");
        }
    }
    private void Play()
    {
        gamePaused = false;
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        GameOverUI.gameObject.SetActive(true);
        GameObject.Find("ScoreEnd").GetComponent<TextMeshProUGUI>().text = "Your score: " + score.ToString();

        //Destroy all remaining bullets to avoid explosion spamming
        GameObject[] bulletList = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bulletList)
        {
            Destroy(bullet);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
        UpdateGameState(GameState.Play);
    }
}
