using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetTrigger : MonoBehaviour
{
    private ItemController itemController;

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

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the collider
        if (other.CompareTag("Player") && itemController != null)
        {
            // Set the item with index 0 as wet
            itemController.SetItemWet(0);
            Debug.Log("Item with index 0 is now wet.");
        }
    }
}
