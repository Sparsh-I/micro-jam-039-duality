using System.Collections;
using TMPro;
using UnityEngine;
using Managers;

namespace Systems
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Wave Settings")] [Tooltip("How long the wave goes on for")] [SerializeField]
        private float waveDuration;

        [Header("Internal Wave Settings")] [Tooltip("How much longer the wave goes on for")] [SerializeField]
        private float waveTimer;

        [Tooltip("Is the wave currently active")] [SerializeField]
        private bool isWaveActive;

        [Tooltip("How many waves have occurred")] [SerializeField]
        private int wavesCompleted;

        [Tooltip("Is the player ready for the next wave?")] [SerializeField]
        private bool isPlayerReadyForNextWave;

        [Header("References")] [Tooltip("Enemy spawner object")] [SerializeField]
        private EnemyController enemySpawner;

        [Tooltip("Shop menu and manager")] [SerializeField]
        private ShopManager shopManager;

        [SerializeField] private TextMeshProUGUI waveText;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(WaveLoop());
            waveText.text = "Wave 1";
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
            wavesCompleted++;
            waveText.text = "Wave " + wavesCompleted;
        }

        private void DestroyAllRemainingEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) Destroy(enemy);
        }
    }
}
