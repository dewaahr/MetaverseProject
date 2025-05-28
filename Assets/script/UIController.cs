using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIController : MonoBehaviour
{
    GameObject player;
    public GameObject MainMenuPanel;
    public GameObject RulersPanel;
    public GameObject PausePanel;
    public GameObject IngamePanel;
    // public string panelName;

    private string PreviousPanelName;

    // private FirstPersonController playerSc;
    private MonoBehaviour playerSc;

    // Start is called before the first frame update
    void Start()


    {
        MainMenuPanel.SetActive(true);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found. Make sure the player has the 'Player' tag.");

        }
        // playerSc.enabled = false; // Disable 
        // the player controller script
        playerSc = player.GetComponent("FirstPersonController") as MonoBehaviour;
        if (playerSc == null)
        {
            Debug.LogError("");
        }
        else
        {
            playerSc.enabled = false; // Disable the player controller script
        }


        // player.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {   
            playerSc.enabled = false; // Disable the player controller script
            PauseButton();
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        MainMenuPanel.SetActive(false);
        RulersPanel.SetActive(true);

        // player.SetActive(true);
    }
    public void RulersBackButton()
    {
        RulersPanel.SetActive(false);

        if (PreviousPanelName == "Panel_PauseMenu")
        {
            EnablePanel(PreviousPanelName);
            PreviousPanelName = "";
            RulersPanel.SetActive(false);
        }
        else
        {
            IngamePanel.SetActive(true);
            playerSc.enabled = true;
        }

            }

    public void PauseButton()
    {
        PausePanel.SetActive(true);
        IngamePanel.SetActive(false);
        PreviousPanelName = "Panel_Ingame";

    }

    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        IngamePanel.SetActive(true);
        playerSc.enabled = true; 
    }
    public void RulesButton()
    {
        PausePanel.SetActive(false);
        RulersPanel.SetActive(true);

        PreviousPanelName = "Panel_Ingame";
    }
    public void PauseToRulesButton()
    {
        PausePanel.SetActive(false);
        RulersPanel.SetActive(true);
        PreviousPanelName = "Panel_PauseMenu";
    }
    public void EnablePanel(string panelName )
    {
        GameObject panel = GameObject.Find(panelName);
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }
    public void DisablePanel(string panelName)
    {
        GameObject panel = GameObject.Find(panelName);
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }

}
