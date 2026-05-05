using UnityEngine;

public enum Role
{
    Civil,
    Police,
    Mafia
}

public class PlayerRole : MonoBehaviour
{
    public Role currentRole = Role.Civil;

    private PlayerKeys playerKeys;

    void Awake()
    {
        playerKeys = GetComponent<PlayerKeys>();
        SetRole(Role.Police);
    }

    public void SetRole(Role newRole)
    {
        currentRole = newRole;
        GiveRoleKeys();
    }

    void GiveRoleKeys()
    {
        if (playerKeys == null)
            return;

        if (currentRole == Role.Police)
        {
            if (!playerKeys.HasKey("PoliceKey"))
                playerKeys.AddKey("PoliceKey");
        }

        if (currentRole == Role.Mafia)
        {
            if (!playerKeys.HasKey("MafiaKey"))
                playerKeys.AddKey("MafiaKey");
        }
    }
}