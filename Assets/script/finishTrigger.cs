using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishTrigger : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // Cari GameManager di scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Periksa apakah pemain memasuki trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the finish line!");
            if (gameManager != null)
            {
                gameManager.GameFinished(); // Panggil metode GameFinished di GameManager
            }
        }
    }
}
