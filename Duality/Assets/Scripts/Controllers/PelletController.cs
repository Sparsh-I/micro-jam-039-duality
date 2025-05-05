using System;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class PelletController : MonoBehaviour
    {
        private Vector3 _spawnPoint;

        [Header("Pellet Object")] [SerializeField]
        private GameObject pelletPrefab;

        [Header("Pellet Spawn Settings")] [Tooltip("Speed at which the pellet gets shot out at")] [SerializeField]
        private float pelletForce;

        [Tooltip("How many pellets get shot out in one time unit")] [SerializeField]
        private float fireRate;

        [Tooltip("What the \"sensor\" range for the pellet to start shooting")] [SerializeField]
        private float range;

        private float _nextPelletTimer;
        
        [Header("Attack Stats UI")]
        [SerializeField] private TextMeshProUGUI fireRateText;
        [SerializeField] private TextMeshProUGUI pelletForceText;
        [SerializeField] private TextMeshProUGUI rangeText;
        
        // Start is called before the first frame update
        void Start()
        {
            _spawnPoint = Vector3.zero;
            fireRateText.text = "FR: " + GetFireRate();
            pelletForceText.text = "PF: " + GetPelletForce();
            rangeText.text = "Range: " + GetRange();
        }

        // Update is called once per frame
        void Update()
        {
            fireRateText.text = "FR: " + GetFireRate();
            pelletForceText.text = "PF: " + GetPelletForce();
            rangeText.text = "Range: " + GetRange();
            
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy)
            {
                Vector3 distanceDifference = closestEnemy.transform.position - _spawnPoint;
                Vector2 direction = distanceDifference.normalized;
                float distanceMagnitude = distanceDifference.magnitude;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

                if (Time.time >= _nextPelletTimer && distanceMagnitude <= range)
                {
                    SpawnPellet(direction);
                    _nextPelletTimer = Time.time + 1 / fireRate;
                }
            }
        }

        private void SpawnPellet(Vector2 pelletDirection)
        {
            GameObject pellet = Instantiate(pelletPrefab, _spawnPoint, Quaternion.identity);
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            rb.velocity = pelletDirection * pelletForce;
        }

        private GameObject FindClosestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float minDistance = Mathf.Infinity;
            Vector3 position = _spawnPoint;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
        
        private double GetPelletForce()
        {
            return Math.Round(pelletForce, 1);
        }

        public void IncreasePelletForce(float force)
        {
            pelletForce += force;
        }

        public void DecreasePelletForce(float force)
        {
            pelletForce -= force;
        }

        private double GetFireRate()
        {
            return Math.Round(fireRate, 1);
        }
        
        public void IncreaseFireRate(float rate)
        {
            fireRate += rate;
        }

        public void DecreaseFireRate(float rate)
        {
            fireRate -= rate;
        }

        private double GetRange()
        {
            return Math.Round(range, 1);
        }

        public void IncreaseRange(float dist)
        {
            range += dist;
        }
        
        public void DecreaseRange(float dist)
        {
            range -= dist;
        }
    }
}
