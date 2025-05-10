using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton instance

    public GameObject heartPrefab; // Prefab for heart icons
    public Transform livesPanel; // Parent object for hearts

    private List<GameObject> heartIcons = new List<GameObject>(); // Stores heart UI objects

    void Awake()
    {
        // Ensure only one UIManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start() // ✅ Move UpdateLivesUI to Start to ensure UI elements exist
    {
        if (heartPrefab == null || livesPanel == null)
        {
            Debug.LogError("UIManager: Missing references! Assign heartPrefab and livesPanel in the Inspector.");
            return;
        }

        UpdateLivesUI(3); // ✅ Now executed after UI is initialized
    }

    public void UpdateLivesUI(int lives)
    {
        Debug.Log($"UIManager: Updating lives UI to {lives} hearts.");

        // Clear existing hearts
        foreach (GameObject heart in heartIcons)
        {
            Destroy(heart);
        }
        heartIcons.Clear();

        // Create new hearts based on lives count
        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, livesPanel);
            if (heart != null)
            {
                heartIcons.Add(heart);
                Debug.Log($"UIManager: Heart {i + 1} created.");
            }
            else
            {
                Debug.LogError($"UIManager: Failed to instantiate heart {i + 1}.");
            }
        }
    }
}
