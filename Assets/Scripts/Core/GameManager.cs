using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        MainMenu,
        LevelSelect,
        Gameplay,
        Paused
    }

    private Dictionary<GameState, string> SceneNames = new()
    {
        { GameState.MainMenu, nameof(GameState.MainMenu) },
        { GameState.LevelSelect, nameof(GameState.LevelSelect) },
        { GameState.Gameplay, nameof(GameState.Gameplay) },
        { GameState.Paused, nameof(GameState.Paused) }
    };

    public GameState CurrentGameState { get; private set; }

    [SerializeField] private float sceneTransitionTime = 1f;

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
    }

    private void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = newState;
        LoadScene(SceneNames[newState]);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // TODO: add anim to load screen;
        yield return new WaitForSeconds(sceneTransitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void StartLevel(int levelNumber)
    {
        CurrentGameState = GameState.Gameplay;

        GameplayState.CurrentLevelNumber = levelNumber;
        LoadScene(SceneNames[GameState.Gameplay]);
    }

    public void OpenLevelSelect()
    {
        ChangeGameState(GameState.LevelSelect);
    }

    public void ReturnToMainMenu()
    {
        ChangeGameState(GameState.MainMenu);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        // TODO: add additional logic
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ChangeGameState(GameState.Gameplay);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}