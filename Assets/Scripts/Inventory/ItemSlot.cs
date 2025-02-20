using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image backgroundImage; // ✅ Background (always visible)
    public Image itemIcon; // ✅ Foreground (turns transparent when empty)
    public TextMeshProUGUI quantityText;
    
    private ItemData currentItem;
    private int quantity = 0;
    private bool isSelected = false; // ✅ Tracks if the slot is selected
    
    private Color defaultBackgroundColor;
    private Color selectedBackgroundColor = new Color(1f, 1f, 0f, 1f); // ✅ Yellow tint
    private Color transparentIconColor = new Color(1f, 1f, 1f, 0f); // ✅ Transparent Icon when empty
    private Color visibleIconColor = new Color(1f, 1f, 1f, 1f); // ✅ Fully visible icon when item is present

    private void Awake()
    {
        if (backgroundImage == null)
        {
            Debug.LogError("ItemSlot: Background Image is missing!");
        }
        else
        {
            defaultBackgroundColor = backgroundImage.color; // ✅ Store default color
        }

        if (itemIcon == null)
        {
            Debug.LogError("ItemSlot: ItemIcon Image is missing!");
        }
        else
        {
            itemIcon.color = transparentIconColor; // ✅ Start fully transparent
        }

        if (quantityText == null)
        {
            quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (quantityText == null)
        {
            Debug.LogError("ItemSlot: Quantity Text is missing from the UI!");
        }
    }

    public void SetItem(ItemData newItem, int amount)
    {
        if (newItem == null)
        {
            Debug.LogError("ItemSlot: Attempted to set a null item!");
            return;
        }

        currentItem = newItem;
        quantity = amount;
        itemIcon.sprite = newItem.icon;
        itemIcon.enabled = true;
        itemIcon.color = visibleIconColor; // ✅ Make icon visible when an item is added
        UpdateQuantity();
    }

    public void ReduceQuantity(int amount)
    {
        if (currentItem == null) return;

        quantity -= amount;

        if (quantity <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateQuantity();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        quantity = 0;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        itemIcon.color = transparentIconColor; // ✅ Hide icon when empty
        if (quantityText != null)
        {
            quantityText.text = "";
        }
    }

    public void UpdateQuantity()
    {
        if (quantityText == null)
        {
            Debug.LogError("ItemSlot: UpdateQuantity() called but quantityText is null!");
            return;
        }

        quantityText.text = (quantity > 1) ? quantity.ToString() : "";
    }

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public bool TryAddItem(ItemData newItem, int amount)
    {
        if (currentItem == null)
        {
            SetItem(newItem, amount);
            return true;
        }
        else if (currentItem == newItem && newItem.isStackable && quantity < newItem.maxStack)
        {
            quantity = Mathf.Min(quantity + amount, newItem.maxStack);
            UpdateQuantity();
            return true;
        }
        return false;
    }

    public ItemData GetItem()
    {
        return currentItem;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if (backgroundImage != null)
        {
            backgroundImage.color = isSelected ? selectedBackgroundColor : defaultBackgroundColor;
        }
    }
}
