using UnityEngine;

public class UiHandle : MonoBehaviour
{
    [SerializeField] private Canvas pickupCanvas; // The UI canvas for the pickup text

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
