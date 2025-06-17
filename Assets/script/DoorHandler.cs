using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private GameObject player;
    // private ItemController itemController;
    public GameObject doorPanel; // The door object to rotate
    public float rotationDuration = 1f; // Duration of the door rotation
    public float openAngle = 90f; // Angle to open the door
    public float interactDistance = 2f; // Distance to interact with the door
    public UiControllerIngame uiControllerIG; // Reference to the UI controller

    private bool isPlayerNearby = false; // To track if the player is within interaction range
    private bool isRotating = false; // To track if the door is currently rotating
    private bool isOpen = false; // To track if the door is open or closed

    private Quaternion closedRotation;
    private Quaternion openRotation;

    ItemController itemController; // Reference to the ItemController script

    void Start()
    {
        // Find the player object and its ItemController
        player = GameObject.FindWithTag("Player");
        itemController = player.GetComponent<ItemController>();

        // Set the door's closed and open rotations
        if (doorPanel != null)
        {   
            closedRotation = doorPanel.transform.rotation;
            openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate the distance between the player and the door
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within interaction range
        if (distance < interactDistance)
        {
            if (!isPlayerNearby)
            {
                // Show the door interaction UI when the player enters the range
                uiControllerIG.enablePanel("doorOpen");
                isPlayerNearby = true;
            }

            // Check for interaction input
            if (Input.GetKeyDown(KeyCode.F) && !isRotating)
            {
                // Check if item 0 is active and wet
                if (itemController != null)
                {
                    int activeItem = itemController.returnActiveItem();
                    if (activeItem != 0 || !itemController.IsFirstItemWet()) // Item 0 must be active AND wet
                    {
                        uiControllerIG.popUpPanel("doorNote");
                        Debug.Log("Item 0 is not wet or not active.");
                        return;
                    }
                }

                // Rotate the door
                StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
                isOpen = !isOpen;
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                // Hide the door interaction UI when the player leaves the range
                uiControllerIG.disablePanel("doorOpen");
                isPlayerNearby = false;
            }
        }
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isRotating = true;
        Quaternion startRotation = doorPanel.transform.rotation;
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            doorPanel.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorPanel.transform.rotation = targetRotation;
        isRotating = false;
    }
}
