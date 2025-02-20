using UnityEngine;
using TMPro;

public class LetterUI : MonoBehaviour
{
    public GameObject letterCanvas; // ✅ Assign LetterCanvas here
    public TextMeshProUGUI letterText; // ✅ Assign the text field inside the UI
    public GameObject livesPanel; // ✅ Reference to LivesPanel
    public GameObject itemBarPanel; // ✅ Reference to ItemBarPanel
    public GameObject itemText; // ✅ Reference to ItemBarPanel

    private void Start()
    {
        letterCanvas.SetActive(false); // ✅ Hide Letter UI at start
    }

    private void Update()
    {
        if (letterCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLetter(); // ✅ Pressing Escape will now close the UI
        }
    }

    public void OpenLetter(string content)
    {
        letterText.text = content; // ✅ Set the text content
        letterCanvas.SetActive(true);
        livesPanel.SetActive(false); // ✅ Hide Lives UI when reading
        itemBarPanel.SetActive(false); // ✅ Hide Item Bar when reading
    }

    public void CloseLetter()
    {
        letterCanvas.SetActive(false);
        livesPanel.SetActive(true); // ✅ Show Lives UI after reading
        itemBarPanel.SetActive(true); // ✅ Show Item Bar after reading
    }
}
