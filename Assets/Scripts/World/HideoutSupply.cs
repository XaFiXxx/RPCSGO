using UnityEngine;

public class HideoutSupply : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private PlayerRole playerRole;
    private PlayerMafia playerMafia;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        PlayerRole role = other.GetComponent<PlayerRole>();
        PlayerMafia mafia = other.GetComponent<PlayerMafia>();
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();

        if (inventory == null ||
            role == null ||
            mafia == null ||
            interaction == null)
        {
            return;
        }

        playerInventory = inventory;
        playerRole = role;
        playerMafia = mafia;

        interaction.EnterSupplyZone(this);

        Debug.Log("Entrée dans la zone de ravitaillement Mafia.");
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();

        if (inventory != null && inventory == playerInventory)
        {
            if (interaction != null)
            {
                interaction.ExitSupplyZone(this);
            }

            playerInventory = null;
            playerRole = null;
            playerMafia = null;

            Debug.Log("Sortie de la zone de ravitaillement Mafia.");
        }
    }

    public void GiveItems()
    {
        if (playerInventory == null ||
            playerRole == null ||
            playerMafia == null)
        {
            return;
        }

        if (playerRole.currentRole != Role.Mafia)
        {
            Debug.Log("Ravitaillement réservé à la Mafia.");
            return;
        }

        int maxSimpleLockpicks;
        int maxExplosiveLockpicks;

        switch (playerMafia.CurrentRank)
        {
            case MafiaRank.ApprentiMafieux:
                maxSimpleLockpicks = 3;
                maxExplosiveLockpicks = 0;
                break;

            case MafiaRank.Mafieux:
                maxSimpleLockpicks = 3;
                maxExplosiveLockpicks = 1;
                break;

            case MafiaRank.BrasDroit:
                maxSimpleLockpicks = 5;
                maxExplosiveLockpicks = 2;
                break;

            case MafiaRank.Parrain:
                maxSimpleLockpicks = 7;
                maxExplosiveLockpicks = 3;
                break;

            case MafiaRank.SousChef:
                maxSimpleLockpicks = 8;
                maxExplosiveLockpicks = 4;
                break;

            case MafiaRank.ChefMafia:
                maxSimpleLockpicks = 8;
                maxExplosiveLockpicks = 4;
                break;

            default:
                maxSimpleLockpicks = 0;
                maxExplosiveLockpicks = 0;
                break;
        }

        int simpleToGive =
            Mathf.Max(
                0,
                maxSimpleLockpicks - playerInventory.simpleLockpicks
            );

        int explosiveToGive =
            Mathf.Max(
                0,
                maxExplosiveLockpicks - playerInventory.explosiveLockpicks
            );

        if (simpleToGive == 0 && explosiveToGive == 0)
        {
            Debug.Log(
                "Tu as déjà le maximum de kits pour ton grade."
            );

            return;
        }

        if (simpleToGive > 0)
        {
            playerInventory.AddSimpleLockpick(simpleToGive);
        }

        if (explosiveToGive > 0)
        {
            playerInventory.AddExplosiveLockpick(explosiveToGive);
        }

        Debug.Log(
            "Ravitaillement Mafia effectué pour le grade : "
            + playerMafia.CurrentRank
        );
    }
}