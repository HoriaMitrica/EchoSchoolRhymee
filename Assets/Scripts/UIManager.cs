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
            Instance = this;
        else
            Destroy(gameObject);
        UpdateLivesUI(3);
    }


    public void UpdateLivesUI(int lives)
    {
        Debug.Log("test "+lives);
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
            heartIcons.Add(heart);
        }
    }
}
