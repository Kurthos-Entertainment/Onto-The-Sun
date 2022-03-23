using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class IngameInfos : MonoBehaviour
{
    public TMPro.TMP_Text FloorValueField;
    public TMPro.TMP_Text ScoreValueField;
    public TMPro.TMP_Text TimeValueField;
    public TMPro.TMP_Text RoundValueField;

    private GameState gameState;

    private void Awake()
    {
        Debug.Assert(FloorValueField != null);
        Debug.Assert(ScoreValueField != null);
        Debug.Assert(TimeValueField != null);
        Debug.Assert(RoundValueField != null);

        Events.onFloorAdded.AddListener(FloorAddedHandler);
    }

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Update()
    {
        ScoreValueField.text = Mathf.Floor(gameState.Score).ToString();
        TimeValueField.text = gameState.GetGameTimeAsClock();
        RoundValueField.text = (gameState.RoundLengthInSeconds - gameState.RoundTime).ToString("F2");
    }

    private void FloorAddedHandler()
    {
        FloorValueField.text = gameState.Floors.ToString();
    }
}