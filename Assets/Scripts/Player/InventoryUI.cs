using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using StarterAssets;

public class InventoryUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventoryPanel;
    public TMP_Text simpleLockpickText;
    public TMP_Text explosiveLockpickText;

    private PlayerInventory inventory;
    private FirstPersonController controller;

    private bool isOpen = false;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        controller = GetComponent<FirstPersonController>();

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }

        if (isOpen)
        {
            UpdateInventoryDisplay();
        }
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(isOpen);

        if (controller != null)
            controller.enabled = !isOpen;

        if (isOpen)
            UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        if (inventory == null)
            return;

        if (simpleLockpickText != null)
        {
            simpleLockpickText.text =
                "Kit de crochetage : x" + inventory.simpleLockpicks;
        }

        if (explosiveLockpickText != null)
        {
            explosiveLockpickText.text =
                "Kit de crochetage explosif : x" + inventory.explosiveLockpicks;
        }
    }
}