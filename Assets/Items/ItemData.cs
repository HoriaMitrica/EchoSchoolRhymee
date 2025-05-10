using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public int maxStack;
    public bool canConsume;
    
    public bool isReadable; // ✅ New field: Can this item be read?
    public string textContent; // ✅ New field: The text displayed when reading it
}
