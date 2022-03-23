using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDNextRound : MonoBehaviour
{
    public TMPro.TMP_Text Value;
    public float UpdateCycleLengthInSeconds = 0.25f;

    private GameState gameState;
    private float lastUpdate = 0;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        Debug.Assert(gameState != null);
    }

    private void Update()
    {
        lastUpdate += Time.deltaTime;
        if (lastUpdate > UpdateCycleLengthInSeconds)
        {
            UpdateValue();
            lastUpdate = 0f;
        }
    }

    private void UpdateValue()
    {
        Value.text = (gameState.RoundLengthInSeconds - gameState.RoundTime).ToString("F0");
    }
}