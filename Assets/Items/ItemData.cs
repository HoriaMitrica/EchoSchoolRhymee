using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName; // Name of the item
    public Sprite icon; // The UI icon
    public bool isStackable; // Can it have a quantity?
    public int maxStack; // Max quantity if stackable
    public bool canConsume; // If true, item is consumed on use
}
