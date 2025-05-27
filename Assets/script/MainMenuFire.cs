using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
     public void StartGame()
    {
        SceneManager.LoadScene(" ");
    }

    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
