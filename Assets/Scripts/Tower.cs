using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class Tower : MonoBehaviour
{
    public GameObject FloorPrefab;
    public GameObject SundiskPrefab;

    private GameObject sundiskInstance;
    private GameState gameState;

    private void Awake()
    {
        Events.onAddFloor.AddListener(AddFloor);
        Events.onGameStarted.AddListener(HandleGameStarted);
        Events.onRoundOver.AddListener(HandleRoundOver);
    }

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Update()
    {
    }

    public void AddFloor()
    {
        Instantiate(FloorPrefab, sundiskInstance.transform.position, Quaternion.identity);
        UpdateSundiskPosition();
        gameState.Floors++;
        Events.onFloorAdded.Invoke();
    }

    private void ResetLevel()
    {
        foreach (GameObject respawn in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            Destroy(respawn);
        }
    }

    private void UpdateSundiskPosition()
    {
        sundiskInstance.transform.position += new Vector3(sundiskInstance.transform.position.x, gameState.FloorHeight, sundiskInstance.transform.position.z);
    }

    private void HandleGameStarted()
    {
        ResetLevel();
        gameState.Floors = 0;
        sundiskInstance = Instantiate(SundiskPrefab, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < gameState.InitialFloorAmount; i++)
        {
            AddFloor();
        }
    }

    private void HandleRoundOver()
    {
        AddFloor();
    }
}