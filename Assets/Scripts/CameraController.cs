using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class CameraController : MonoBehaviour
{
    public float MaxCameraPosition = 7f;
    public float MinCameraPosition = 7f;
    public float InitialScrollSpeed = 10f;
    public float ScrollSpeedIncrease = 1f;
    public float ScreenBorderTriggerShare = 0.1f;
    public GameObject Rag;
    public GameObject SlashPrefab;

    private GameState gameState;
    private float scrollSpeed = 0;

    private void Awake()
    {
        Debug.Assert(Rag != null);
        Debug.Assert(SlashPrefab != null);

        Events.onFloorAdded.AddListener(HandleFloorAdded);
        Events.onGameStarted.AddListener(HandleGameStarted);
    }

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Update()
    {
        CheckMouseDown();
        CheckMainMenu();
        CheckScrolling();
    }

    private void CheckScrolling()
    {
        float topScreenTrigger = Screen.height * (1 - ScreenBorderTriggerShare);
        float bottomScreenTrigger = Screen.height * ScreenBorderTriggerShare;
        float verticalMousePosition = Input.mousePosition.y;

        if (verticalMousePosition >= topScreenTrigger)
        {
            transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }

        if (verticalMousePosition <= bottomScreenTrigger)
        {
            transform.position -= new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, MinCameraPosition, MaxCameraPosition), transform.position.z);
    }

    private void CheckMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            if (gameState.GamePaused && !gameState.GameOver && gameState.GameStarted)
            {
                Events.onGameResumed.Invoke();
            }
            else
            {
                Events.onGamePaused.Invoke();
            }
        }
    }

    private void CheckMouseDown()
    {
        if (!gameState.GamePaused)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Cursor.visible = false;
                Rag.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                Rag.SetActive(true);
            }
            else
            {
                Rag.SetActive(false);
                Cursor.visible = true;
            }
        }
    }

    private void HandleGameStarted()
    {
        MaxCameraPosition = 7f;
        scrollSpeed = InitialScrollSpeed;
    }

    private void HandleFloorAdded()
    {
        MaxCameraPosition += gameState.FloorHeight;
        scrollSpeed += ScrollSpeedIncrease;
    }
}