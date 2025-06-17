using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        UiControllerIngame uiController = FindObjectOfType<UiControllerIngame>();
        if (uiController != null)
        {
            uiController.isGameOver = true; // Tandai bahwa game sudah berakhir
            uiController.ShowGameOverPanel();
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

        // Kembali ke menu utama setelah 5 detik
        StartCoroutine(ReturnToMainMenuAfterDelay(5f));
    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene menu utama Anda
    }
}
