using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // ✅ Fixes the missing 'Image' error

public class ItemBar : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform itemBarPanel;
    private List<ItemSlot> slots = new List<ItemSlot>();
    private int selectedIndex = 0; // Selected inventory slot

    void Start()
    {
        GenerateSlots();
    }

    private void GenerateSlots()
    {
        if (slotPrefab == null || itemBarPanel == null)
        {
            Debug.LogError("ItemBar: SlotPrefab or ItemBarPanel is missing!");
            return;
        }

        slots.Clear(); // Clear existing slots to prevent duplicates

        for (int i = 0; i < 9; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, itemBarPanel);
            ItemSlot slotComponent = newSlot.GetComponent<ItemSlot>();

            if (slotComponent == null)
            {
                Debug.LogError($"ItemBar: Slot {i} is missing ItemSlot script!");
                continue;
            }

            slots.Add(slotComponent);
        }

        Debug.Log($"ItemBar: Successfully created {slots.Count} slots.");
        UpdateSelection();
    }

    void Update()
    {
        HandleSlotSelection();
        HandleItemUsage();
    }

    private void HandleSlotSelection()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)))
            {
                selectedIndex = i;
                UpdateSelection();
                break;
            }
        }
    }

    private void HandleItemUsage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSelectedItem();
        }
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image slotImage = slots[i].GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.color = (i == selectedIndex) ? Color.yellow : Color.white;
            }
        }
    }

    private void UseSelectedItem()
    {
        if (slots[selectedIndex] == null || slots[selectedIndex].IsEmpty())
        {
            Debug.Log("ItemBar: No item in selected slot!");
            return;
        }

        ItemSlot selectedSlot = slots[selectedIndex];
        ItemData selectedItem = selectedSlot.GetItem();

        if (selectedItem == null)
        {
            Debug.LogError("ItemBar: Selected item data is null!");
            return;
        }

        Debug.Log($"Using {selectedItem.itemName}...");

        if (selectedItem.canConsume)
        {
            selectedSlot.ReduceQuantity(1);
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        if (slots == null || slots.Count == 0)
        {
            Debug.LogError("ItemBar: Slots list is empty!");
            return false;
        }

        Debug.Log($"ItemBar: Trying to add {amount}x {item.itemName}");

        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsEmpty() && slot.TryAddItem(item, amount))
            {
                Debug.Log($"ItemBar: Stacked {amount}x {item.itemName}.");
                return true;
            }
        }

        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item, amount);
                Debug.Log($"ItemBar: Added {amount}x {item.itemName} to an empty slot.");
                return true;
            }
        }

        Debug.Log("ItemBar: Inventory full, could not add item.");
        return false;
    }
}
