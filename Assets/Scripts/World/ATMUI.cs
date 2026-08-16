using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class ATMUI : MonoBehaviour
{
    [Header("ATM UI")]
    public GameObject atmPanel;

    public TMP_Text cashText;
    public TMP_Text bankBalanceText;

    public TMP_InputField amountInput;

    public Button depositButton;
    public Button withdrawButton;
    public Button closeButton;

    private PlayerWallet currentWallet;
    private FirstPersonController controller;

    void Start()
    {
        if (atmPanel != null)
            atmPanel.SetActive(false);

        if (depositButton != null)
            depositButton.onClick.AddListener(Deposit);

        if (withdrawButton != null)
            withdrawButton.onClick.AddListener(Withdraw);

        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
    }

    public void Open(PlayerWallet wallet)
    {
        if (wallet == null)
            return;

        currentWallet = wallet;

        controller = wallet.GetComponent<FirstPersonController>();

        if (controller != null)
            controller.enabled = false;

        if (atmPanel != null)
            atmPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (amountInput != null)
            amountInput.text = "";

        UpdateDisplay();
    }

    public void Close()
    {
        if (atmPanel != null)
            atmPanel.SetActive(false);

        if (controller != null)
            controller.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentWallet = null;
        controller = null;
    }

    private void Deposit()
    {
        if (currentWallet == null)
            return;

        if (!TryGetAmount(out int amount))
            return;

        bool success =
            currentWallet.DepositCash(amount);

        if (success)
        {
            amountInput.text = "";
            UpdateDisplay();
        }
    }

    private void Withdraw()
    {
        if (currentWallet == null)
            return;

        if (!TryGetAmount(out int amount))
            return;

        bool success =
            currentWallet.WithdrawCash(amount);

        if (success)
        {
            amountInput.text = "";
            UpdateDisplay();
        }
    }

    private bool TryGetAmount(out int amount)
    {
        amount = 0;

        if (amountInput == null)
            return false;

        if (!int.TryParse(amountInput.text, out amount))
        {
            Debug.Log("Montant invalide.");
            return false;
        }

        if (amount <= 0)
        {
            Debug.Log("Le montant doit être supérieur à 0.");
            return false;
        }

        return true;
    }

    private void UpdateDisplay()
    {
        if (currentWallet == null)
            return;

        if (cashText != null)
        {
            cashText.text =
                "Argent liquide : " + currentWallet.Cash + " $";
        }

        if (bankBalanceText != null)
        {
            bankBalanceText.text =
                "Solde bancaire : " + currentWallet.BankBalance + " $";
        }
    }
}