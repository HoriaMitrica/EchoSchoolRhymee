using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public string sceneName = "TestLevel"; // Set your scene name
    public GameObject button; // Assign the UI button
        public GameObject text; // Assign the text
        public PlayerCombat player; // Assign the text

    public GameObject background; // Assign the background
    void Update()
    {
        if(player._lives==0)
        {
            background.SetActive(true);
        }
    }
    public void Respawn()
    {
        // Hide the UI elements
        if (button != null) button.SetActive(false);
        if (background != null) background.SetActive(false);
                if (text != null) text.SetActive(false);


        // Restart the scene
        SceneManager.LoadScene(sceneName);
    }
}
