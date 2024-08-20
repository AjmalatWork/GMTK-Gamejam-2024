using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    private RespawnPoint activeRespawnPoint;
    private List<RespawnPoint> removedRespawnPoints;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of RespawnManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scene loads
            removedRespawnPoints = new List<RespawnPoint>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActiveRespawnPoint(RespawnPoint newRespawnPoint)
    {
        // if new point was already activated once, it cannot be activated again
        if(removedRespawnPoints.Contains(newRespawnPoint))
        {
            return;
        }

        // Deactivate the current active respawn point if it exists
        if (activeRespawnPoint != null)
        {
            activeRespawnPoint.Deactivate();
            removedRespawnPoints.Add(activeRespawnPoint);
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
            player.transform.rotation = activeRespawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogWarning("No active respawn point found!");
        }
    }
}
