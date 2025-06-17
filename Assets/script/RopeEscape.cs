using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEscape : MonoBehaviour
{
    private ItemController itemController; // Reference to the ItemController script
    private bool isPlayerInCollider = false; // Track if the player is in the collider
    public GameObject ropePrefab; // Reference to the escape effect prefab

    void Start()
    {
        // Find the player and get the ItemController component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            itemController = player.GetComponent<ItemController>();
            if (itemController == null)
            {
                Debug.LogError("ItemController not found on Player.");
            }
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }

    void Update()
    {
        // Check if the player is in the collider and has item 2 (index 1)
        if (isPlayerInCollider && itemController != null)
        {
            if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to escape
            {
                ropePrefab.SetActive(true); // Activate the escape effect
                int activeItem = itemController.returnActiveItem();
                if (activeItem == 1) // Check if item 2 (index 1) is active
                {
                    Escape();
                }
                else
                {
                    Debug.Log("You need item 2 (index 1) to escape.");
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInCollider = true;
            Debug.Log("Player entered the escape area.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInCollider = false;
            Debug.Log("Player left the escape area.");
        }
    }

    private void Escape()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameFinished(); // Call the GameFinished method in GameManager
            Debug.Log("Player escaped successfully!");
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }
}
