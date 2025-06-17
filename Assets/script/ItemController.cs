using UnityEngine;

public class ItemController : MonoBehaviour
{
    private bool[] isItemPickedUp; // Array to track if items are picked up
    private bool[] isItemWet; // Array to track if items are wet
    private int currentIndex = -1; // Tracks the currently active item
    public GameObject[] items; // Array of item GameObjects
    private GameObject player;
    private ItemController itemController;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
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

        isItemPickedUp = new bool[items.Length]; // Initialize picked-up status for all items
        isItemWet = new bool[items.Length]; // Inisialisasi status basah untuk semua item
    }

    void Update()
    {
        // Check for input to switch items
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && items.Length > 1)
        {
            ActivateItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && items.Length > 2)
        {
            ActivateItem(2);
        }
    }

    public void ActivateItem(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            // Check if the item has been picked up
            if (!IsItemPickedUp(index))
            {
                Debug.Log($"Item {index + 1} has not been picked up yet.");
                return; // Do not activate the item if it hasn't been picked up
            }

            // Deactivate the currently active item
            if (currentIndex >= 0 && currentIndex < items.Length)
            {
                items[currentIndex].SetActive(false);
            }

            // Activate the selected item
            currentIndex = index;
            items[index].SetActive(true);
            Debug.Log($"Item {index + 1} is now active.");
        }
    }

    public void PickUpItem(int index)
    {
        if (index >= 0 && index < isItemPickedUp.Length)
        {
            isItemPickedUp[index] = true; // Mark the item as picked up
            Debug.Log($"Item {index + 1} has been picked up.");
        }
    }

    public bool IsItemPickedUp(int index)
    {
        if (index >= 0 && index < isItemPickedUp.Length)
        {
            return isItemPickedUp[index]; // Check if the item is picked up
        }
        return false;
    }

    public void SetItemWet(int index)
    {
        if (index >= 0 && index < isItemWet.Length)
        {
            isItemWet[index] = true; // Tandai item sebagai basah
            Debug.Log($"Item {index} is now wet.");
        }
        else
        {
            Debug.LogError($"Invalid index {index} for SetItemWet.");
        }
    }

    public bool IsFirstItemWet()
    {
        return isItemWet.Length > 0 && isItemWet[0]; // Check if the first item is wet
    }

    public int returnActiveItem()
    {
        return currentIndex; // Return the currently active item index
    }
}
