using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // Cari GameManager di scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Periksa apakah pemain menyentuh api
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by fire! Game Over.");
            if (gameManager != null)
            {
                // gameManager.GameOver(); // Panggil metode GameOver di GameManager
                
            }
        }
    }
}