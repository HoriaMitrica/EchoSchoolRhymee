using UnityEngine;
using TMPro;

public class LetterUI : MonoBehaviour
{
    public GameObject letterCanvas; // ✅ Assign LetterCanvas here
    public TextMeshProUGUI letterText; // ✅ Assign the text field inside the UI
    public GameObject livesPanel; // ✅ Reference to LivesPanel
    public GameObject itemBarPanel; // ✅ Reference to ItemBarPanel
    public GameObject itemText; // ✅ Reference to ItemText

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
        if (itemText != null) itemText.SetActive(false); // ✅ Hide ItemText
        letterText.text = content; // ✅ Set letter content
        letterCanvas.SetActive(true);
        livesPanel.SetActive(false); // ✅ Hide Lives UI
        itemBarPanel.SetActive(false); // ✅ Hide Item Bar
    }

    public void CloseLetter()
    {
        letterCanvas.SetActive(false);
        livesPanel.SetActive(true); // ✅ Show Lives UI
        itemBarPanel.SetActive(true); // ✅ Show Item Bar
        if (itemText != null) itemText.SetActive(true); // ✅ Restore ItemText
    }
}
