using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("FirstFloor"); // Replace with your main game scene name
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}