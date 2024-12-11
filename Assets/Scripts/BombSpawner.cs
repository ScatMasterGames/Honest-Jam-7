using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnIntervalRandomness = 0.5f;
    [SerializeField] float spawnIntervalDecrease = 0.1f;
    [SerializeField] float minSpawnInterval = 0.05f;
    [SerializeField] float hardnessIncreaseInterval = 10f;
    [SerializeField] private float minYPosition = 0;
    [SerializeField] private float maxYPosition = 0;
    
    public UnityEvent OnBombSpawned;
    
    private float nextDifficultyTime;

    private void Start()
    {
        Invoke(nameof(SpawnBomb),2f);
    }
    
    void SpawnBomb()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(minYPosition, maxYPosition), 0);
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        OnBombSpawned.Invoke();
        
        float nextSpawnTime = spawnInterval + Random.Range(-spawnIntervalRandomness, spawnIntervalRandomness);
        nextSpawnTime = Mathf.Max(0, nextSpawnTime);
        Invoke(nameof(SpawnBomb), nextSpawnTime);
    }
}
