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

    public GameObject gameOverPanel; // Tambahkan referensi ke panel Game Over

    public GameObject doorNotePanel;
    public GameObject RopeNotePanel;
    public GameObject AparNotePanel;
    public GameObject doorOpenPanel;
    public GameObject itemPickupPanel;

    public GameObject item1Box;
    public GameObject item2Box;
    public GameObject item3Box;


    // AreaTriggerCont hand
    public GameObject ShiftTrigger;
    public GameObject RopeTrigger;
    public GameObject AparTrigger;
    public GameObject WetRagPanel;

    public bool isPaused = false;
    public bool isGameOver = false; // Tambahkan variabel untuk melacak status game over

    void Awake()
    {
        // RulesPanel.SetActive(true);
        // IngamePanel.SetActive(true);
        // pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Cegah pause menu jika game sudah berakhir
        if (isGameOver) return;

        // Toggle pause menu ketika tombol Escape ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void enablePanel(string panelName)
    {
        Debug.Log($"enablePanel called with panelName: {panelName}");

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
            case "ShiftTrigger":
                panelToShow = ShiftTrigger;
                break;
            case "RopeTrigger":
                panelToShow = RopeTrigger;
                break;
            case "AparTrigger":
                panelToShow = AparTrigger;
                break;
            case "WetRag":
                panelToShow = WetRagPanel;
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

    public void PauseGame()
    {
        // Cegah pause menu jika game sudah berakhir
        if (isGameOver) return;

        isPaused = true;
        pausePanel.SetActive(true); // Tampilkan menu pause

        // Nonaktifkan kontrol pemain
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            FirstPersonController playerController = player.GetComponent<FirstPersonController>();
            if (playerController != null)
            {
                playerController.enabled = false; // Nonaktifkan kontrol pemain
            }
        }

        Cursor.lockState = CursorLockMode.None; // Bebaskan kursor
        Cursor.visible = true; // Tampilkan kursor
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Sembunyikan menu pause

        // Aktifkan kembali kontrol pemain
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            FirstPersonController playerController = player.GetComponent<FirstPersonController>();
            if (playerController != null)
            {
                playerController.enabled = true; // Aktifkan kontrol pemain
            }
        }

        Cursor.lockState = CursorLockMode.Locked; // Kunci kursor
        Cursor.visible = false; // Sembunyikan kursor
    }

    // public void QuitGame()
    // {
    //     Time.timeScale = 1f; // Ensure the game is running
    //     Application.Quit(); // Quit the application

    //     #if UNITY_EDITOR
    //     UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
    //     #endif
    // }
    public void restartGame()
    {
        Time.timeScale = 1f; // Ensure the game is running
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }
    public void backToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is running
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    public void ShowGameOverPanel()
    {
        StartCoroutine(ZoomInPanel(gameOverPanel, 0.5f)); // Zoom in selama 0.5 detik
    }

    private IEnumerator ZoomInPanel(GameObject panel, float duration)
    {
        panel.SetActive(true); // Aktifkan panel
        panel.transform.localScale = Vector3.zero; // Set skala awal ke 0

        Vector3 targetScale = Vector3.one; // Skala akhir (1, 1, 1)
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Interpolasi skala dari 0 ke 1
            panel.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);
            yield return null;
        }

        panel.transform.localScale = targetScale; // Pastikan skala akhir adalah (1, 1, 1)
    }
}
