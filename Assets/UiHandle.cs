using UnityEngine;
using TMPro;

public class UiHandle : MonoBehaviour
{
    [SerializeField] private Canvas pickupCanvas; // The UI canvas for the pickup text
    [SerializeField] private TextMeshProUGUI itemNameText; // ✅ Item name text
    [SerializeField] private TextMeshProUGUI itemDescriptionText; // ✅ Item description text

    private void Awake()
    {
        if (pickupCanvas != null)
        {
            pickupCanvas.gameObject.SetActive(false); // ✅ Hide UI at start
        }
        else
        {
            Debug.LogError("UiHandle: No Canvas assigned! Assign the pickup text Canvas.");
        }
    }

    public void SetPickupText(string itemName, string itemDescription)
    {
        if (itemNameText != null)
        {
            itemNameText.text = itemName;
        }
        else
        {
            Debug.LogError("UiHandle: Item name TextMeshProUGUI is missing!");
        }

        if (itemDescriptionText != null)
        {
            itemDescriptionText.text = itemDescription;
        }
        else
        {
            Debug.LogError("UiHandle: Item description TextMeshProUGUI is missing!");
        }
    }

    public void CanvasEnable()
    {
        if (pickupCanvas != null)
        {
            pickupCanvas.gameObject.SetActive(true);
        }
    }

    public void CanvasDisable()
    {
        if (pickupCanvas != null)
        {
            pickupCanvas.gameObject.SetActive(false);
        }
    }
}
