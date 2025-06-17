using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerCont : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public GameObject areaTrigger;
    public string areaName;
    bool isTriggered = false;
    // public UiControllerIngame uiControllerIG; // Reference to the in-game UI controller
    // GameObject Canvas;
    public UiControllerIngame uiControllerIG; // Reference to the in-game UI controller
    // private ItemController itemController; // Reference to the ItemController script
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Assign itemController from the player or another relevant GameObject
        // uiControllerIG = GameObject.Find("Canvas").GetComponent<UiControllerIngame>();

    }
    // Update is called once per frame
    void Update()
    {
        if (player != null && areaTrigger != null)
        {
            if (areaTrigger.GetComponent<Collider>().bounds.Contains(player.transform.position))
            {
                if (!isTriggered)
                {
                    isTriggered = true;
                    uiControllerIG.popUpPanel(areaName);
                }
            }
            else
            {
                isTriggered = false; // Reset when the player leaves the area
            }
        }
    }
}

