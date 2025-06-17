
using UnityEngine;

using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject RulesPanel;

    void Awake()
    {
        MainMenuPanel.SetActive(true);
        RulesPanel.SetActive(false);

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
        }
    }

    


    public void ExitButton()
    {
        Application.Quit();
    }
    
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
