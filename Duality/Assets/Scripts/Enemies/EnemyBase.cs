using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Enemies
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [Tooltip("Max health of the enemy")] [SerializeField]
        protected float health;

        [Tooltip("Level of the enemy")] [SerializeField]
        protected float level;

        [Tooltip("Damage the enemy deals to the player")] [SerializeField]
        protected int attackStat;

        private void Start()
        {
            SetAttackStat();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().TakeDamage(attackStat);
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0) Destroy(gameObject);
        }

        public abstract float GetManaDrop();

        public abstract void SetAttackStat();
    }
}
