using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    private ItemData currentItem;
    private int quantity = 0;

    private void Awake()
    {
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
}
