using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("Current player health")]
    [SerializeField] private float health;
    
    [Tooltip("Max player health")]
    [SerializeField] private float maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void restoreHealth()
    {
        health = maxHealth;
    }

    private float getHealth()
    {
        return health;
    }

    private float getMaxHealth()
    {
        return maxHealth;
    }
    
    
}
