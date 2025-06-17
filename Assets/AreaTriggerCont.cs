using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerCont : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public GameObject areaTrigger;
    public string areaName;
    bool isTriggered = false;
    public UiControllerIngame uiControllerIG; // Reference to the in-game UI controller
    private ItemController itemController; // Reference to the ItemController script
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Assign itemController from the player or another relevant GameObject
        if (player != null)
        {
            itemController = player.GetComponent<ItemController>();
            if (itemController == null)
            {
                Debug.LogError("ItemController component not found on Player.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && areaTrigger != null)
        {
            // Check if the player is within the area trigger
            if (areaTrigger.GetComponent<Collider>().bounds.Contains(player.transform.position))
            {
                // Player is inside the area trigger
                // Debug.Log("Player entered the area trigger: " + areaName);
                if (!isTriggered)
                {
                    isTriggered = true;
                    // Call the method to handle the area trigger
                    if (areaName == "WetRag")
                    {
                        Debug.Log("WetRag area triggered");
                        itemController.SetItemWet(0);
                    }
                    uiControllerIG.enablePanel(areaName);
                    
                    }
                }
            }
        }
    }

