using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform policeSpawn;
    public Transform mafiaSpawn;
    public Transform civilSpawn;

    private PlayerRole playerRole;

    void Start()
    {
        playerRole = GetComponent<PlayerRole>();

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Transform spawnPoint = civilSpawn;

        if (playerRole.currentRole == Role.Police)
            spawnPoint = policeSpawn;

        else if (playerRole.currentRole == Role.Mafia)
            spawnPoint = mafiaSpawn;

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
}