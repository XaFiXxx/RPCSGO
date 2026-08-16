using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("HUD")]
    public TMP_Text cashText;
    public TMP_Text healthText;
    public TMP_Text armorText;

    private PlayerWallet wallet;
    private PlayerHealth playerHealth;

    void Start()
    {
        wallet = GetComponent<PlayerWallet>();
        playerHealth = GetComponent<PlayerHealth>();

        UpdateHUD();
    }

    void Update()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        // =========================
        // ARGENT LIQUIDE
        // =========================
        if (cashText != null && wallet != null)
        {
            cashText.text =
                "Cash : " + wallet.Cash + " $";
        }

        // =========================
        // VIE
        // =========================
        if (healthText != null && playerHealth != null)
        {
            healthText.text =
                "Vie : " + playerHealth.CurrentHealth + " PV";
        }

        // =========================
        // KEVLAR
        // =========================
        if (armorText != null && playerHealth != null)
        {
            armorText.text =
                "Kevlar : " + playerHealth.CurrentArmor + " %";
        }
    }
}