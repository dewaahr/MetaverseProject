using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject FireContainer;
    public GameObject SmokeContainer;

    public void GameOver()
    {
        UiControllerIngame uiController = FindObjectOfType<UiControllerIngame>();
        if (uiController != null)
        {
            uiController.isGameOver = true; // Tandai bahwa game sudah berakhir
            disableContainer(); // Nonaktifkan kontainer api dan asap
            uiController.ShowGameOverPanel();
        }

        // Nonaktifkan kontrol pemain
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            FirstPersonController playerController = player.GetComponent<FirstPersonController>();
            if (playerController != null)
            {
                playerController.enabled = false;

                // Nonaktifkan kontrol pemain
            }
        }
        Cursor.lockState = CursorLockMode.None; // Bebaskan kursor
        Cursor.visible = true; // Tampilkan kursor

        // Kembali ke menu utama setelah 5 detik
        StartCoroutine(ReturnToMainMenuAfterDelay(5f));
    }

    public void GameFinished()
    {
        UiControllerIngame uiController = FindObjectOfType<UiControllerIngame>();
        if (uiController != null)
        {
            uiController.isGameFinished = true; // Tandai bahwa game sudah selesai
            enableContainer(); // Aktifkan kontainer api dan asap
            uiController.ShowGameFinishedPanel();
        }

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
        // Kembali ke menu utama setelah 5 detik
        StartCoroutine(ReturnToMainMenuAfterDelay(5f));

    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene menu utama Anda
    }

    public void disableContainer()
    {
        FireContainer.SetActive(false);
        SmokeContainer.SetActive(false);
    }
    public void enableContainer()
    {
        FireContainer.SetActive(true);
        SmokeContainer.SetActive(true);
    }
}
