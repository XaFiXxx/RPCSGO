using UnityEngine;

public enum MafiaRank
{
    ApprentiMafieux = 0,
    Mafieux = 1,
    BrasDroit = 2,
    Parrain = 3,
    SousChef = 4,
    ChefMafia = 5
}

public class PlayerMafia : MonoBehaviour
{
    [Header("Mafia")]
    [SerializeField] private MafiaRank currentRank = MafiaRank.ApprentiMafieux;

    private PlayerRole playerRole;

    public MafiaRank CurrentRank => currentRank;

    void Awake()
    {
        playerRole = GetComponent<PlayerRole>();
    }

    public bool IsMafia()
    {
        return playerRole != null &&
               playerRole.currentRole == Role.Mafia;
    }

    public void SetRank(MafiaRank newRank)
    {
        if (!IsMafia())
        {
            Debug.LogWarning(
                "Impossible de changer le grade Mafia : le joueur n'est pas Mafia."
            );

            return;
        }

        currentRank = newRank;

        Debug.Log("Nouveau grade Mafia : " + currentRank);
    }

    public bool HasMinimumRank(MafiaRank minimumRank)
    {
        if (!IsMafia())
            return false;

        return currentRank >= minimumRank;
    }

    public bool IsRank(MafiaRank rank)
    {
        if (!IsMafia())
            return false;

        return currentRank == rank;
    }
}