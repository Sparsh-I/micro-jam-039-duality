using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    private Vector3 _spawnPoint;
    
    [Header("Pellet Object")]
    [SerializeField] private GameObject pelletPrefab;
    
    [Header("Pellet Spawn Settings")]
    [Tooltip("Speed at which the pellet gets shot out at.")]
    [SerializeField] private float pelletForce;
    
    [Tooltip("How often the pellet gets shot out.")]
    [SerializeField] private float fireRate;
    
    [Tooltip("Time in seconds for when the next pellet gets shot out.")]
    [SerializeField] private float nextPelletTimer;
    
    [Header("Pellet Rotation Settings")]
    [Tooltip("Speed at which the pellet spawner spins.")]
    [SerializeField] private float spinSpeed;
    
    [Tooltip("Current angle of the pellet rotation.")]
    [SerializeField] private float currentShotAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnPoint = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        currentShotAngle += Time.deltaTime * spinSpeed;
        Vector2 direction = new Vector2(Mathf.Cos(currentShotAngle * Mathf.Deg2Rad), Mathf.Sin(currentShotAngle * Mathf.Deg2Rad));
        if (Time.time >= nextPelletTimer)
        {
            SpawnPellet(direction);
            nextPelletTimer = Time.time + fireRate;
        }
    }

    private void SpawnPellet(Vector2 pelletDirection)
    {
        GameObject pellet = Instantiate(pelletPrefab, _spawnPoint, Quaternion.identity);
        Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
        rb.velocity = pelletDirection * pelletForce;
    }
}
