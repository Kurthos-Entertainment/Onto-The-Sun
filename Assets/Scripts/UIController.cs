using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class UIController : MonoBehaviour
{
    public AudioSource GameOverAudio;
    public Canvas GameOverCanvas;
    public Canvas UpgradeCanvas;

    private bool gameOverAudioPlayed = false;

    private void Awake()
    {
        Events.onGameOver.AddListener(HandleGameOver);
        Events.onGamePaused.AddListener(HandleGamePaused);
        Events.onGameResumed.AddListener(HandleGameResumed);
        Events.onGameStarted.AddListener(HandleGameStarted);
        Events.onRoundOver.AddListener(HandleRoundOver);
        Events.onRoundStarted.AddListener(HandleRoundStarted);
    }

    private void Start()
    {
    }

    private void HandleGameOver()
    {
        if (!gameOverAudioPlayed)
        {
            GameOverCanvas.gameObject.SetActive(true);
            GameOverAudio.Play();
            gameOverAudioPlayed = true;
        }
    }

    private void HandleGamePaused()
    {
        GameOverCanvas.gameObject.SetActive(false);
    }

    private void HandleGameResumed()
    {
    }

    private void HandleGameStarted()
    {
        GameOverCanvas.gameObject.SetActive(false);
        gameOverAudioPlayed = false;
    }

    private void HandleRoundOver()
    {
        UpgradeCanvas.gameObject.SetActive(true);
    }

    private void HandleRoundStarted()
    {
        UpgradeCanvas.gameObject.SetActive(false);
    }
}