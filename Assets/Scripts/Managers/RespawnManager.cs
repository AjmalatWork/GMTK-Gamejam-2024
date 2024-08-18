using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    private RespawnPoint activeRespawnPoint;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of RespawnManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActiveRespawnPoint(RespawnPoint newRespawnPoint)
    {
        // Deactivate the current active respawn point if it exists
        if (activeRespawnPoint != null)
        {
            activeRespawnPoint.Deactivate();
        }

        // Set the new respawn point as active
        activeRespawnPoint = newRespawnPoint;
        activeRespawnPoint.Activate();
    }

    public void RespawnPlayer(GameObject player)
    {
        if (activeRespawnPoint != null)
        {
            player.transform.position = activeRespawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No active respawn point found!");
        }
    }
}
