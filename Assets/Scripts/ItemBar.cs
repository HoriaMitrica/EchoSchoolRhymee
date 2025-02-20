using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; // ✅ Import TextMeshPro

public class ItemBar : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform itemBarPanel;
    public TextMeshProUGUI itemText; // ✅ Public parameter for ItemText

    private List<ItemSlot> slots = new List<ItemSlot>();
    private int selectedIndex = 0;
    
    public LetterUI letterUI; // ✅ Reference to Letter UI

    void Start()
    {
        GenerateSlots(); // ✅ Ensure this method exists
        letterUI = FindObjectOfType<LetterUI>(); // ✅ Ensure `LetterUI` is found at runtime
        UpdateItemText(); // ✅ Set initial text
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

    private void Update()
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
                UpdateItemText(); // ✅ Update item text when switching slots
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
            slots[i].SetSelected(i == selectedIndex);
        }
    }

    private void UpdateItemText()
    {
        if (itemText == null)
        {
            Debug.LogError("ItemBar: ItemText reference is missing!");
            return;
        }

        if (slots[selectedIndex] == null || slots[selectedIndex].IsEmpty())
        {
            itemText.text = ""; // ✅ Empty if no item
        }
        else
        {
            ItemSlot selectedSlot = slots[selectedIndex];
            ItemData selectedItem = selectedSlot.GetItem();
            itemText.text = selectedItem != null ? selectedItem.itemName : ""; // ✅ Update with item name
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

        if (selectedItem.isReadable) // ✅ Open Letter UI for readable items
        {
            if (letterUI != null)
            {
                letterUI.OpenLetter(selectedItem.textContent);
            }
            else
            {
                Debug.LogError("ItemBar: LetterUI is missing! Ensure it exists in the scene.");
            }
            return; // ✅ Do not reduce quantity when reading
        }

        if (selectedItem.canConsume)
        {
            selectedSlot.ReduceQuantity(1);
        }

        UpdateItemText(); // ✅ Update text after using an item
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
                UpdateItemText(); // ✅ Update text after adding an item
                return true;
            }
        }

        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item, amount);
                Debug.Log($"ItemBar: Added {amount}x {item.itemName} to an empty slot.");
                UpdateItemText(); // ✅ Update text after adding an item
                return true;
            }
        }

        Debug.Log("ItemBar: Inventory full, could not add item.");
        return false;
    }
}
