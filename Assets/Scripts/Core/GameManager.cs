using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    public GridManager GridManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public CardManager CardManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }

    public int CurrentTurn { get; private set; }
    public int CurrentLevel { get; private set; }

    public event Action OnGameStart;
    public event Action OnTurnEnd;
    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GridManager = GetComponent<GridManager>();
        UIManager = GetComponent<UIManager>();
        CardManager = GetComponent<CardManager>();
        ScoreManager = GetComponent<ScoreManager>();
    }

    public void StartNewGame()
    {
        CurrentState = GameState.Playing;
        CurrentTurn = 1;
        CurrentLevel = 1;
        OnGameStart?.Invoke();
        OnGameStateChanged?.Invoke(CurrentState);
    }

    public void EndTurn()
    {
        CurrentTurn++;
        OnTurnEnd?.Invoke();
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            CurrentState = GameState.Paused;
            Time.timeScale = 0;
            OnGameStateChanged?.Invoke(CurrentState);
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            CurrentState = GameState.Playing;
            Time.timeScale = 1;
            OnGameStateChanged?.Invoke(CurrentState);
        }
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        OnGameStateChanged?.Invoke(CurrentState);
    }

    public void SaveGame()
    {
    }

    public void LoadGame()
    {
    }
}