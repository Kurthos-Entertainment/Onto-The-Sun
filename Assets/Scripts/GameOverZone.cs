using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;

public class GameOverZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sundisk")
        {
            Events.onGameOver.Invoke();
        }
    }
}