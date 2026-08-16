using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private int cash = 0;
    [SerializeField] private int bankBalance = 0;

    public int Cash => cash;
    public int BankBalance => bankBalance;

    public void AddCash(int amount)
    {
        if (amount <= 0)
            return;

        cash += amount;

        Debug.Log("Argent liquide : " + cash + "$");
    }

    public bool RemoveCash(int amount)
    {
        if (amount <= 0)
            return false;

        if (cash < amount)
        {
            Debug.Log("Pas assez d'argent liquide.");
            return false;
        }

        cash -= amount;

        Debug.Log("Argent liquide : " + cash + "$");

        return true;
    }

    public void AddToBank(int amount)
    {
        if (amount <= 0)
            return;

        bankBalance += amount;

        Debug.Log("Solde bancaire : " + bankBalance + "$");
    }

    public bool RemoveFromBank(int amount)
    {
        if (amount <= 0)
            return false;

        if (bankBalance < amount)
        {
            Debug.Log("Solde bancaire insuffisant.");
            return false;
        }

        bankBalance -= amount;

        Debug.Log("Solde bancaire : " + bankBalance + "$");

        return true;
    }

    public bool DepositCash(int amount)
    {
        if (!RemoveCash(amount))
            return false;

        AddToBank(amount);

        Debug.Log("Dépôt effectué : " + amount + "$");

        return true;
    }

    public bool WithdrawCash(int amount)
    {
        if (!RemoveFromBank(amount))
            return false;

        AddCash(amount);

        Debug.Log("Retrait effectué : " + amount + "$");

        return true;
    }
}