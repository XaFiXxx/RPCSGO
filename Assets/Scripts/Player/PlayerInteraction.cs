using UnityEngine;
using UnityEngine.InputSystem;
using DoorScript;

public class PlayerInteraction : MonoBehaviour
{
    public float range = 3f;

    private PlayerKeys playerKeys;
    private PlayerInventory inventory;

    void Start()
    {
        playerKeys = GetComponent<PlayerKeys>();
        inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Door door = hit.collider.GetComponent<Door>();

            if (door != null)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    door.OpenDoor();
                }

                if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    door.ToggleLock(playerKeys);
                }

                if (Keyboard.current.gKey.wasPressedThisFrame)
                {
                    door.TrySimpleLockpick(inventory);
                }

                if (Keyboard.current.hKey.wasPressedThisFrame)
                {
                    door.TryExplosiveLockpick(inventory);
                }

                return;
            }

            HideoutSupply supply = hit.collider.GetComponent<HideoutSupply>();

            if (supply != null)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    supply.GiveItems(inventory);
                }

                return;
            }
        }
    }
}