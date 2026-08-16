using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DoorScript;
using StarterAssets;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float range = 3f;

    [Header("Lockpick UI")]
    public GameObject lockpickUI;
    public Slider lockpickProgress;
    public TMP_Text lockpickText;

    private PlayerKeys playerKeys;
    private PlayerInventory inventory;
    private PlayerWallet wallet;
    private FirstPersonController controller;

    private bool isLockpicking = false;

    private HideoutSupply currentSupply;

    void Start()
    {
        controller = GetComponent<FirstPersonController>();
        playerKeys = GetComponent<PlayerKeys>();
        inventory = GetComponent<PlayerInventory>();
        wallet = GetComponent<PlayerWallet>();

        if (lockpickUI != null)
            lockpickUI.SetActive(false);

        if (lockpickProgress != null)
            lockpickProgress.value = 0f;
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (isLockpicking)
            return;

        RaycastHit hit;

        if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            range))
        {
            // =========================
            // PORTES
            // =========================
            Door door =
                hit.collider.GetComponentInParent<Door>();

            if (door != null)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                    door.OpenDoor();

                if (Keyboard.current.fKey.wasPressedThisFrame)
                    door.ToggleLock(playerKeys);

                if (Keyboard.current.gKey.wasPressedThisFrame)
                {
                    StartCoroutine(
                        SimpleLockpickRoutine(door)
                    );
                }

                if (Keyboard.current.hKey.wasPressedThisFrame)
                {
                    StartCoroutine(
                        ExplosiveLockpickRoutine(door)
                    );
                }

                return;
            }

            // =========================
            // ATM
            // =========================
            ATM atm =
                hit.collider.GetComponentInParent<ATM>();

            if (atm != null)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    atm.Interact(wallet);
                }

                return;
            }
        }

        // =========================
        // RAVITAILLEMENT MAFIA
        // =========================
        if (currentSupply != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentSupply.GiveItems();
        }
    }

    public void EnterSupplyZone(HideoutSupply supply)
    {
        currentSupply = supply;
    }

    public void ExitSupplyZone(HideoutSupply supply)
    {
        if (currentSupply == supply)
            currentSupply = null;
    }

    private IEnumerator SimpleLockpickRoutine(Door door)
    {
        if (!door.CanStartSimpleLockpick(inventory))
            yield break;

        isLockpicking = true;

        StartLockpickUI("Crochetage...");

        float elapsed = 0f;
        float duration = door.simpleLockpickDuration;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (lockpickProgress != null)
            {
                lockpickProgress.value =
                    Mathf.Clamp01(elapsed / duration);
            }

            yield return null;
        }

        bool success =
            door.ResolveSimpleLockpick(inventory);

        if (success)
        {
            if (lockpickText != null)
            {
                lockpickText.text =
                    "Crochetage réussi !";
            }

            yield return new WaitForSeconds(0.5f);

            door.OpenDoor();
        }
        else
        {
            if (lockpickText != null)
            {
                lockpickText.text =
                    "Crochetage échoué.";
            }

            yield return new WaitForSeconds(0.8f);
        }

        StopLockpickUI();
    }

    private IEnumerator ExplosiveLockpickRoutine(Door door)
    {
        if (!door.CanStartExplosiveLockpick(inventory))
            yield break;

        isLockpicking = true;

        StartLockpickUI("Forçage de la serrure...");

        float elapsed = 0f;
        float duration = door.explosiveLockpickDuration;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (lockpickProgress != null)
            {
                lockpickProgress.value =
                    Mathf.Clamp01(elapsed / duration);
            }

            yield return null;
        }

        bool success =
            door.ResolveExplosiveLockpick(inventory);

        if (success)
        {
            if (lockpickText != null)
            {
                lockpickText.text =
                    "Serrure forcée !";
            }

            yield return new WaitForSeconds(0.5f);

            door.OpenDoor();
        }
        else
        {
            if (lockpickText != null)
            {
                lockpickText.text =
                    "Forçage échoué !";
            }

            yield return new WaitForSeconds(0.8f);
        }

        StopLockpickUI();
    }

    private void StartLockpickUI(string message)
    {
        if (controller != null)
            controller.enabled = false;

        if (lockpickUI != null)
            lockpickUI.SetActive(true);

        if (lockpickProgress != null)
            lockpickProgress.value = 0f;

        if (lockpickText != null)
            lockpickText.text = message;
    }

    private void StopLockpickUI()
    {
        if (lockpickUI != null)
            lockpickUI.SetActive(false);

        if (lockpickProgress != null)
            lockpickProgress.value = 0f;

        if (controller != null)
            controller.enabled = true;

        isLockpicking = false;
    }
}