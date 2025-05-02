using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private Vector3 _spawnPoint;
    [SerializeField] private float maxDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnPoint = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _spawnPoint);
        if (distance >= maxDistance)
            Destroy(gameObject);
    }
}
