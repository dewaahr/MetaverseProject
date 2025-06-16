using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    ItemController itemController;
    public int itemIndex;
    // public string itemName;
    UiControllerIngame uiControllerIG;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        itemController = player.GetComponent<ItemController>();
        uiControllerIG = GameObject.Find("Canvas").GetComponent<UiControllerIngame>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 1f) // Jarak interaksi
        {
            // uiControllerIG.popUpPanel("itemPickup");
            uiControllerIG.enablePanel("ItemPickup");
            // uiControllerIG.itemPickupPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && player != null)
            {
                itemController.ToggleItem(itemIndex);
                uiControllerIG.disablePanel("ItemPickup");

                Destroy(gameObject);
            }

        }
        // uiControllerIG.disablePanel
    }
    

}
