using UnityEngine;
using UnityEngine.Events;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float initialDelay = 2f;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float spawnIntervalRandomness = 0f;

    [Header("Random Y Position")]
    [SerializeField] bool randomYPosition = false;
    [SerializeField] private float minYPosition = 0;
    [SerializeField] private float maxYPosition = 0;

    public UnityEvent OnBombSpawned;
    
    private void Start()
    {
        Invoke(nameof(SpawnBomb),initialDelay);
    }
    
    void SpawnBomb()
    {
        Vector3 spawnPosition;
        if (randomYPosition)
        {
            spawnPosition = new Vector3(transform.position.x, Random.Range(minYPosition, maxYPosition), 0);
        }
        else
        {
            spawnPosition = transform.position;
        }
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        OnBombSpawned.Invoke();
        
        float nextSpawnTime = spawnInterval + Random.Range(-spawnIntervalRandomness, spawnIntervalRandomness);
        nextSpawnTime = Mathf.Max(0, nextSpawnTime);
        Invoke(nameof(SpawnBomb), nextSpawnTime);
    }
}
