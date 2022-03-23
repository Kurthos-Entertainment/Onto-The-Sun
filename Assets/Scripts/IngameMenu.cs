using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyEventSystem;

public class IngameMenu : MonoBehaviour
{
    private void Awake()
    {
        Events.onGameOver.AddListener(HandleGameOver);
        Events.onGamePaused.AddListener(HandleGamePaused);
        Events.onGameResumed.AddListener(HandleGameResumed);
        Events.onGameStarted.AddListener(HandleGameStarted);
    }

    public void AddFloor()
    {
        Events.onAddFloor.Invoke();
    }

    private void SetButtonInteractibility(bool flag)
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            button.interactable = flag;
        }
    }

    private void HandleGameOver()
    {
        SetButtonInteractibility(false);
    }

    private void HandleGamePaused()
    {
        SetButtonInteractibility(false);
    }

    private void HandleGameResumed()
    {
        SetButtonInteractibility(true);
    }

    private void HandleGameStarted()
    {
        SetButtonInteractibility(true);
    }
}