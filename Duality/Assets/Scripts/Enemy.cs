using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Max health of the enemy")]
    [SerializeField] private float health;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
