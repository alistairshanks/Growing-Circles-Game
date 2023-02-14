using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameState currentGameState = GameState.menu;

    [SerializeField] GameObject inGameUi;
    [SerializeField] GameObject menuUi;
    [SerializeField] GameObject gameOverUi;
    [SerializeField] ScoreManager scoreManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetGameState(GameState.menu);
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        scoreManager.StartGame();
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }


    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            inGameUi.gameObject.SetActive(false);
            gameOverUi.gameObject.SetActive(false);
            menuUi.gameObject.SetActive(true);

        }

        if (newGameState == GameState.inGame)
        {
            inGameUi.gameObject.SetActive(true);
            gameOverUi.gameObject.SetActive(false);
            menuUi.gameObject.SetActive(false);


        }

        if (newGameState == GameState.gameOver)
        {
            inGameUi.gameObject.SetActive(false);
            gameOverUi.gameObject.SetActive(true);
            menuUi.gameObject.SetActive(false);

        }

        currentGameState = newGameState;
    }
}
