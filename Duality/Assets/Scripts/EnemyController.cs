using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Vector3 _endPoint;
    
    [Header("List of Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Enemy Spawn Settings")]
    [Tooltip("Radius at which the enemies spawn from.")]
    [SerializeField] private float spawnRadius;
    
    [Tooltip("Speed at which the enemy moves at.")]
    [SerializeField] private float enemySpeed;
    
    [Tooltip("How often the enemies spawn.")]
    [SerializeField] private float spawnRate;
    
    [Tooltip("Time in seconds for when the next enemy spawns in.")]
    [SerializeField] private float nextEnemyTimer;

    [Header("Enemy Selector")]
    [Tooltip("Lower bound (inclusive) for spawning smallest enemies.")]
    [SerializeField] private int minRandomiserValue;
    
    [Tooltip("Upper bound (exclusive) for spawning largest enemies.")]
    [SerializeField] private int maxRandomiserValue;
    
    [Tooltip("Largest value (inclusive) for spawning smallest enemies.")]
    [SerializeField] private float enemySmallThreshold;
    
    [Tooltip("Largest value (inclusive) for spawning medium enemies.")]
    [SerializeField] private float enemyMediumThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        _endPoint = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextEnemyTimer)
        {
            SpawnEnemy();
            nextEnemyTimer = Time.time + spawnRate;
        }
    }
    
    private void SpawnEnemy() 
    {
        int enemySelector = Random.Range(minRandomiserValue, maxRandomiserValue);
        if (enemySelector <= enemySmallThreshold) 
            SpawnEnemy(enemyPrefabs[0]);
        else if (enemySelector <= enemyMediumThreshold)
            SpawnEnemy(enemyPrefabs[1]);
        else
            SpawnEnemy(enemyPrefabs[2]);
    }
    
    private void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPosition = GetRandomPointOnCircle();
        GameObject enemyInstance = Instantiate(enemy, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = enemyInstance.GetComponent<Rigidbody2D>();

        Vector3 directionToCenter = (_endPoint - spawnPosition).normalized;
        rb.velocity = directionToCenter * enemySpeed;
        
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        enemyInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
    
    public Vector3 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, 360f);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;

        return new Vector3(x, y, 0f);
    }
}
