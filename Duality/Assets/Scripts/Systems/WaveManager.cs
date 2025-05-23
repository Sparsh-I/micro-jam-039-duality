using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Managers;
using UnityEngine.Serialization;

namespace Systems
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Wave Settings")] [Tooltip("How long the wave goes on for")] [SerializeField]
        private float waveDuration;

        [Tooltip("Which wave are is the current one?")] [SerializeField]
        private int currentWave;
        
        [Header("Internal Wave Settings")] [Tooltip("How much longer the wave goes on for")] [SerializeField]
        private float waveTimer;

        [Tooltip("Is the wave currently active")] [SerializeField]
        private bool isWaveActive;

        [Tooltip("Is the player ready for the next wave?")] [SerializeField]
        private bool isPlayerReadyForNextWave;

        [Header("References")] [Tooltip("Enemy spawner object")] [SerializeField]
        private EnemyController enemySpawner;

        [Tooltip("Shop menu and manager to start/stop between waves")] [SerializeField]
        private ShopManager shopManager;

        [SerializeField] private TextMeshProUGUI waveCounterText;
        [SerializeField] private TextMeshProUGUI waveTimerText;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(WaveLoop());
            waveCounterText.text = "Wave 1";
            waveTimerText.text = GetWaveTimer();
        }

        // Update is called once per frame
        void Update()
        {
            waveTimerText.text = GetWaveTimer();
            
            if (isWaveActive)
            {
                waveTimer -= Time.deltaTime;
                enemySpawner.SpawnEnemyWave(currentWave);

                if (waveTimer <= 0) EndWave();
            }
        }

        private string GetWaveTimer()
        {
            return Math.Round(waveTimer, 0).ToString("F0");;
        }
        
        IEnumerator WaveLoop()
        {
            while (true)
            {
                StartWave();
                yield return new WaitForSeconds(waveDuration);
                EndWave();

                isPlayerReadyForNextWave = false;
                yield return new WaitUntil(() => isPlayerReadyForNextWave);
            }
        }

        private void StartWave()
        {
            waveTimer = waveDuration;
            isWaveActive = true;
        }

        private void EndWave()
        {
            DestroyAllRemainingEnemies();
            isWaveActive = false;
            shopManager.OpenShop();
        }

        public void BeginNextWave()
        {
            shopManager.CloseShop();
            isPlayerReadyForNextWave = true;
            currentWave++;
            waveCounterText.text = "Wave " + currentWave;
        }

        private void DestroyAllRemainingEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) Destroy(enemy);
        }
    }
}
