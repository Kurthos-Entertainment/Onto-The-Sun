using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class GameState : MonoBehaviour
{
    [Header("Settings")]
    public float BeamMaxCondition = 100f;
    public float DecayMaxInitial = 5f;
    public float DecayMinInitial = 0f;
    public float DecayMultiplicator = 1f;
    public float FloorHeight = 5;
    public float InitialFloorAmount = 2;
    public float RoundLengthInSeconds = 15;
    public float ScrubRepair = 5f;

    [Header("States")]
    public bool GamePaused = true;
    public bool GameOver = false;
    public bool GameStarted = false;
    public float GameTime = 0;
    public float Score = 0;
    public float RoundTime = 0;
    public int Floors = 0;

    private void Awake()
    {
        Events.onGameOver.AddListener(HandleGameOver);
        Events.onGamePaused.AddListener(HandleGamePaused);
        Events.onGameResumed.AddListener(HandleGameResumed);
        Events.onGameStarted.AddListener(HandleGameStarted);
        Events.onRoundOver.AddListener(HandleRoundOver);
        Events.onRoundStarted.AddListener(HandleRoundStarted);
        Events.onUpgradeSelected.AddListener(HandleUgpradeSelected);
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        CheckRoundTime();
        UpdateGameTime();
        UpdateScore();
    }

    private void CheckRoundTime()
    {
        if (!GamePaused)
        {
            if (RoundTime > RoundLengthInSeconds)
            {
                Events.onRoundOver.Invoke();
                RoundTime = 0;
            }
        }
    }

    private void UpdateGameTime()
    {
        if (!GameOver && !GamePaused)
        {
            GameTime += Time.deltaTime;
            RoundTime += Time.deltaTime;
        }
    }

    private void UpdateScore()
    {
        if (!GameOver)
        {
            Score += Floors * Time.deltaTime;
        }
    }

    private void HandleGameOver()
    {
        GameOver = true;
        Time.timeScale = 0.3f;
    }

    private void HandleGamePaused()
    {
        GamePaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    private void HandleGameResumed()
    {
        GamePaused = false;
        Time.timeScale = 1;
    }

    private void HandleGameStarted()
    {
        Score = 0;
        GamePaused = false;
        GameOver = false;
        GameStarted = true;
        Time.timeScale = 1;
    }

    private void HandleRoundOver()
    {
        GamePaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    private void HandleRoundStarted()
    {
        GamePaused = false;
        Time.timeScale = 1;
    }

    private void HandleUgpradeSelected(string upgrade)
    {
        Debug.Log($"Upgrade selected: {upgrade}");
        switch (upgrade)
        {
            case "morepoints":
                Score *= 1.2f;
                break;

            case "lessdecay":
                DecayMultiplicator *= 0.95f;
                break;

            case "morescrub":
                ScrubRepair *= 1.15f;
                break;

            case "morehp":
                BeamMaxCondition *= 1.1f;
                break;
        }
        Events.onRoundStarted.Invoke();
    }

    public int GetGameTimeInSeconds()
    {
        return (int)Mathf.Floor(GameTime);
    }

    public string GetGameTimeAsClock()
    {
        float hours = Mathf.Floor(GameTime / 3600);
        float minutes = Mathf.Floor((GameTime - (hours * 3600)) / 60);
        float seconds = Mathf.Floor(GameTime - (hours * 3600) - (minutes * 60));
        return $"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}";
    }
}