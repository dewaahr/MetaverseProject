using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControllerIngame : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject RulesPanel;
    public GameObject IngamePanel;
    public GameObject pausePanel;

    public GameObject doorNotePanel;
    public GameObject RopeNotePanel;
    public GameObject AparNotePanel;
    public GameObject doorOpenPanel;
    public GameObject itemPickupPanel;

    public GameObject item1Box;
    public GameObject item2Box;
    public GameObject item3Box;





    private bool isPaused = false;
    private bool isGameOver = false;
    private bool isStarted = false;

    void Awake()
    {
        // RulesPanel.SetActive(true);
        IngamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void enablePanel(string panelName)
    {
        switch (panelName)
        {
            case "Rules":
                // MainMenuPanel.SetActive(false);
                RulesPanel.SetActive(true);
                break;
            case "ItemPickup":
                itemPickupPanel.SetActive(true);
                break;
            case "doorOpen":
                doorOpenPanel.SetActive(true);
                break;
            case "doorNote":
                doorNotePanel.SetActive(true);
                break;
            case "RopeNote":
                RopeNotePanel.SetActive(true);
                break;
            case "AparNote":
                AparNotePanel.SetActive(true);
                break;
            case "IngamePanel":
                IngamePanel.SetActive(true);
                break;
            case "item1":
                item1Box.SetActive(true);
                break;
            case "item2":
                item2Box.SetActive(true);
                break;
            case "item3":
                item3Box.SetActive(true);
                break;
        }
    }

    public void disablePanel(string panelName)
    {
        switch (panelName)
        {
            case "Rules":
                // MainMenuPanel.SetActive(true);
                RulesPanel.SetActive(false);
                break;
            case "ItemPickup":
                itemPickupPanel.SetActive(false);
                break;
            case "doorOpen":
                doorOpenPanel.SetActive(false);
                break;
            case "doorNote":
                doorNotePanel.SetActive(false);
                break;
            case "RopeNote":
                RopeNotePanel.SetActive(false);
                break;
            case "AparNote":
                AparNotePanel.SetActive(false);
                break;
            case "IngamePanel":
                IngamePanel.SetActive(false);
                break;
            case "item1":
                item1Box.SetActive(false);
                break;
            case "item2":
                item2Box.SetActive(false);
                break;
            case "item3":
                item3Box.SetActive(false);
                break;

        }
    }
    
    public void popUpPanel(string panelName)
    {
        GameObject panelToShow = null;
        switch (panelName)
        {
            case "doorNote":
                panelToShow = doorNotePanel;
                break;
            case "RopeNote":
                panelToShow = RopeNotePanel;
                break;
            case "AparNote":
                panelToShow = AparNotePanel;
                break;
            case "doorOpen":
                panelToShow = doorOpenPanel;
                break;
            case "itemPickup":
                panelToShow = itemPickupPanel;
                break;
        }
        if (panelToShow != null)
        {
            StartCoroutine(ShowPanelForSeconds(panelToShow, 3f));
        }
    }

    private IEnumerator ShowPanelForSeconds(GameObject panel, float seconds)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        panel.SetActive(false);
    }

}
