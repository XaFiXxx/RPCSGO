using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int simpleLockpicks = 0;
    public int explosiveLockpicks = 0;

    public void AddSimpleLockpick(int amount)
    {
        simpleLockpicks += amount;
        Debug.Log("Lockpick simple: " + simpleLockpicks);
    }

    public void AddExplosiveLockpick(int amount)
    {
        explosiveLockpicks += amount;
        Debug.Log("Lockpick explosif: " + explosiveLockpicks);
    }

    public bool UseSimpleLockpick()
    {
        if (simpleLockpicks > 0)
        {
            simpleLockpicks--;
            return true;
        }

        Debug.Log("Pas de lockpick simple");
        return false;
    }

    public bool UseExplosiveLockpick()
    {
        if (explosiveLockpicks > 0)
        {
            explosiveLockpicks--;
            return true;
        }

        Debug.Log("Pas de lockpick explosif");
        return false;
    }
}