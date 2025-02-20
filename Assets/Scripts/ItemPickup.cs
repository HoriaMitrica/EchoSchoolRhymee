using UnityEngine;

namespace Items.pickups
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private ItemData itemData; // Scriptable Object reference
        [SerializeField] private int amount = 1;
        [SerializeField] private UiHandle uiHandle;
        [SerializeField] private string customItemName; // ✅ Public field for custom name
        [SerializeField] private string customDescription; // ✅ Public field for custom description

        private bool _hasUi;
        private SpriteRenderer _sprite;
        private PlayerMovement _playerMovement;
        private ItemBar _itemBar;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            if (_sprite != null && itemData != null)
            {
                _sprite.sprite = itemData.icon;
            }
            else
            {
                Debug.LogError("ItemPickup: Missing SpriteRenderer or ItemData reference!");
            }

            _hasUi = uiHandle != null;
            _itemBar = FindObjectOfType<ItemBar>();

            if (_itemBar == null)
            {
                Debug.LogError("ItemPickup: No ItemBar found in the scene! Ensure it's present.");
            }

            if (_hasUi)
            {
                uiHandle.CanvasDisable();
            }
        }

        private void Update()
        {
            if (_playerMovement != null && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player pressed E to pick up the item!");

                if (_itemBar != null)
                {
                    bool added = _itemBar.AddItem(itemData, amount);
                    if (added)
                    {
                        Debug.Log($"Picked up {amount}x {itemData.itemName}");
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventory full!");
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered pickup range!");
                _playerMovement = other.GetComponent<PlayerMovement>();

                if (_playerMovement != null && _hasUi)
                {
                    string displayName = string.IsNullOrEmpty(customItemName) ? itemData.itemName : customItemName;
                    string displayDescription = string.IsNullOrEmpty(customDescription) ? $"Press E to pick up {amount}x {itemData.itemName}" : customDescription;
                    
                    uiHandle.SetPickupText(displayName, displayDescription);
                    uiHandle.CanvasEnable();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player left pickup range!");
                _playerMovement = null;

                if (_hasUi)
                {
                    uiHandle.CanvasDisable();
                }
            }
        }
    }
}
