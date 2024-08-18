using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public bool isActive = false; // Tracks whether this respawn point is active

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            RespawnManager.Instance.SetActiveRespawnPoint(this);
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;        
    }
}
