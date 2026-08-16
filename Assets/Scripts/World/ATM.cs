using UnityEngine;

public class ATM : MonoBehaviour
{
    [Header("ATM UI")]
    public ATMUI atmUI;

    public void Interact(PlayerWallet wallet)
    {
        if (wallet == null)
            return;

        if (atmUI == null)
        {
            Debug.LogWarning("ATMUI non assigné sur l'ATM.");
            return;
        }

        atmUI.Open(wallet);
    }
}