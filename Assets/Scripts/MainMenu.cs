using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyEventSystem;

public class MainMenu : MonoBehaviour
{
    public Button ResumeButton;
    public GameObject HUD;

    private void Awake()
    {
        Events.onGameOver.AddListener(HandleGameOver);
        Events.onGamePaused.AddListener(HandleGamePaused);
        Events.onGameResumed.AddListener(HandleGameResumed);
        Events.onGameStarted.AddListener(HandleGameStarted);
    }

    public void NewGame()
    {
        Events.onGameStarted.Invoke();
    }

    public void ResumeGame()
    {
        Events.onGameResumed.Invoke();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void HandleGameOver()
    {
        ResumeButton.interactable = false;
    }

    private void HandleGamePaused()
    {
        gameObject.SetActive(true);
    }

    private void HandleGameResumed()
    {
        gameObject.SetActive(false);
    }

    private void HandleGameStarted()
    {
        HUD.gameObject.SetActive(true);
        ResumeButton.interactable = true;
        gameObject.SetActive(false);
    }
}