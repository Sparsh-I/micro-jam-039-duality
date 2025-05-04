using Enemies;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private Vector3 _spawnPoint;
    
    [Tooltip("Max distance that the pellet can travel before despawning")]
    [SerializeField] private float maxDistance;
    
    [Tooltip("Damage multiplier to be applied to the pellet's force/speed")]
    [SerializeField] private float damageMultiplier;
    
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnPoint = Vector3.zero;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _spawnPoint);
        if (distance >= maxDistance) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(GetPelletDamage());
            Destroy(gameObject);
        }
    }

    private float GetPelletDamage()
    {
        return _rb.velocity.magnitude * damageMultiplier;
    }
}
