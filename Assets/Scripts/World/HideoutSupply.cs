using UnityEngine;

public class HideoutSupply : MonoBehaviour
{
    public int simpleAmount = 8;
    public int explosiveAmount = 3;

    public void GiveItems(PlayerInventory inventory)
    {
        inventory.AddSimpleLockpick(simpleAmount);
        inventory.AddExplosiveLockpick(explosiveAmount);

        Debug.Log("Items récupérés !");
    }
}