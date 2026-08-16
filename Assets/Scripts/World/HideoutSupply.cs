using UnityEngine;

public class HideoutSupply : MonoBehaviour
{
    [Header("Stock maximum donné par le QG")]
    public int maxSimpleLockpicks = 5;
    public int maxExplosiveLockpicks = 3;

    private PlayerInventory playerInventory;
    private PlayerRole playerRole;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        PlayerRole role = other.GetComponent<PlayerRole>();
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();

        if (inventory == null || role == null || interaction == null)
            return;

        playerInventory = inventory;
        playerRole = role;

        interaction.EnterSupplyZone(this);

        Debug.Log("Entrée dans la zone de ravitaillement.");
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        PlayerInteraction interaction = other.GetComponent<PlayerInteraction>();

        if (inventory != null && inventory == playerInventory)
        {
            if (interaction != null)
                interaction.ExitSupplyZone(this);

            playerInventory = null;
            playerRole = null;

            Debug.Log("Sortie de la zone de ravitaillement.");
        }
    }

    public void GiveItems()
    {
        if (playerInventory == null || playerRole == null)
            return;

        if (playerRole.currentRole != Role.Mafia)
        {
            Debug.Log("Ravitaillement réservé à la Mafia.");
            return;
        }

        int simpleToGive =
            Mathf.Max(0, maxSimpleLockpicks - playerInventory.simpleLockpicks);

        int explosiveToGive =
            Mathf.Max(0, maxExplosiveLockpicks - playerInventory.explosiveLockpicks);

        if (simpleToGive == 0 && explosiveToGive == 0)
        {
            Debug.Log("Tu as déjà le maximum de kits.");
            return;
        }

        if (simpleToGive > 0)
            playerInventory.AddSimpleLockpick(simpleToGive);

        if (explosiveToGive > 0)
            playerInventory.AddExplosiveLockpick(explosiveToGive);

        Debug.Log("Ravitaillement Mafia effectué.");
    }
}