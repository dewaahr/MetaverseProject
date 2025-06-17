using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private UiControllerIngame uiController; // Reference to UiControllerIngame
    private GameManager gameManager; // Reference to GameManager

    void Start()
    {
        // Find the UiControllerIngame in the scene
        uiController = FindObjectOfType<UiControllerIngame>();
        if (uiController == null)
        {
            Debug.LogError("UiControllerIngame not found in the scene.");
        }

        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pause the timer if the game is paused
        if (uiController != null && uiController.isPaused)
        {
            return; // Do nothing while the game is paused
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 6f)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = Color.white; 
            }
        }
        else
        {
            remainingTime = 0f; // Stop the timer at 0

            // Trigger game over when the timer reaches 0
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
        }


        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
