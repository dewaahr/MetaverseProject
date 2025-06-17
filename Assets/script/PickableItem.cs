using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private GameObject player;
    ItemController itemController;
    public int itemIndex; // Index of the item in the player's inventory
    public string itemName; // Name of the item for display purposes
    public UiControllerIngame uiControllerIG; // Reference to the UI controller
    private bool isPlayerNearby = false; // To track if the player is within interaction range

    void Start()
    {
        // Find the player object and its ItemController
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            itemController = player.GetComponent<ItemController>();
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate the distance between the player and the item
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within interaction range
        if (distance < 1f) // Interaction range
        {
            if (!isPlayerNearby)
            {
                // Show the pickup UI panel when the player enters the range
                uiControllerIG.enablePanel("ItemPickup");
                isPlayerNearby = true;
            }

            // Check for interaction input
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Add the item to the player's inventory
                itemController.PickUpItem(itemIndex);

                // Disable the UI panel and destroy the item
                uiControllerIG.disablePanel("ItemPickup");

                Destroy(gameObject);
                uiControllerIG.enablePanel(itemName);
                if (itemName == "item1")
                {

                    // If the first item is picked up, set it as wet
                    // itemController.SetItemWet(itemIndex);
                    uiControllerIG.popUpPanel("WetRag");
                    if (itemIndex == 0)
                    {
                        // itemController.SetItemWet(itemIndex);
                        Debug.Log("Item 0 has been set to wet.");
                    }
                    else
                    {
                        Debug.Log("This item cannot be set to wet.");
                    }
                }
                Debug.Log($"Picked up {itemName} (Index: {itemIndex})");
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                // Hide the pickup UI panel when the player leaves the range
                uiControllerIG.disablePanel("ItemPickup");
                isPlayerNearby = false;
            }
        }
    }
}
