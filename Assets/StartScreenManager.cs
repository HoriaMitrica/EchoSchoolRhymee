using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start Button Clicked! Loading Game Scene...");
        SceneManager.LoadScene("Level1"); // Replace with your actual game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked! Exiting Game...");
        Application.Quit();
    }
}
