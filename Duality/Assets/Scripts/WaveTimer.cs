using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [Header("Wave Settings")] 
    [Tooltip("How long the wave goes on for")]
    [SerializeField] private float waveDuration;
    
    [Tooltip("How long between the different waves")]
    [SerializeField] private float breakBetweenWaves;

    [Header("Internal Wave Settings")]
    [Tooltip("How much longer the wave goes on for")]
    [SerializeField] private float waveTimer;
    
    [Tooltip("Is the wave currently active")]
    [SerializeField] private bool isWaveActive;

    [Header("Enemy Spawner")]
    [SerializeField] private EnemyController enemySpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaveActive)
        {
            waveTimer -= Time.deltaTime;
            enemySpawner.SpawnEnemyWave();
            
            if (waveTimer <= 0) EndWave();
        }
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            StartWave();
            yield return new WaitForSeconds(waveDuration);
            EndWave();
            yield return new WaitForSeconds(breakBetweenWaves); // Change to wait between ShopManager (not created)
                                                                // inputs, such as button to continue instead of a timer
        }
    }

    void StartWave()
    {
        waveTimer = waveDuration;
        isWaveActive = true;
    }

    void EndWave()
    {
        isWaveActive = false;
    }
}
