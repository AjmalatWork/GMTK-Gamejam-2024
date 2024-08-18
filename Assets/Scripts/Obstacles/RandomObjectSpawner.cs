using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    [SerializeField] private string[] objectTags; // Tags corresponding to objects in the pool
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnRangeX = 8f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), spawnInterval, spawnInterval);
    }

    private void SpawnObject()
    {
        // Select a random tag
        string selectedTag = objectTags[Random.Range(0, objectTags.Length)];

        // Determine a random X position within the range
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(transform.position.x + spawnPosX, transform.position.y, 0);

        // Spawn the object from the pool
        ObjectPooler.Instance.SpawnFromPool(selectedTag, spawnPos, Quaternion.identity);
    }
}
