using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DoorScript;
using StarterAssets;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float range = 3f;
    public TMP_Text lockpickText;

    private PlayerKeys playerKeys;
    private PlayerInventory inventory;
    private bool isLockpicking = false;
    private FirstPersonController controller;

    void Start()
    {
        controller = GetComponent<FirstPersonController>();
        playerKeys = GetComponent<PlayerKeys>();
        inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;
        if (isLockpicking) return;

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
                    StartCoroutine(SimpleLockpickRoutine(door));
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

    private IEnumerator SimpleLockpickRoutine(Door door)
    {
        isLockpicking = true;

        if (controller != null)
        controller.enabled = false;

        Debug.Log("Crochetage en cours...");


        Debug.Log("Crochetage en cours...");

        yield return new WaitForSeconds(door.simpleLockpickDuration);

        door.ResolveSimpleLockpick(inventory);

        if (controller != null)
        controller.enabled = true;

        isLockpicking = false;
    }
}