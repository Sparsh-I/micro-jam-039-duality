using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Health")] [Tooltip("Current player health")] [SerializeField]
        private float health;

        [Tooltip("Max player health")] [SerializeField]
        private float maxHealth;

        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Points")] [SerializeField] private PointSystem pointSystem;

        [SerializeField] private TextMeshProUGUI attackPointsText;

        [SerializeField] private TextMeshProUGUI defencePointsText;

        void Start()
        {
            health = maxHealth;
            healthText.text = "Health: " + GetHealth() + "/" + GetMaxHealth();
            attackPointsText.text = "AP: 0/" + pointSystem.GetMaxAttackPoints();
            defencePointsText.text = "DP: 0/" + pointSystem.GetMaxDefencePoints();
        }

        void Update()
        {
            healthText.text = "Health: " + GetHealth() + "/" + GetMaxHealth();
            attackPointsText.text = "AP: " + pointSystem.GetAttackPoints() + "/" + pointSystem.GetMaxAttackPoints();
            defencePointsText.text = "DP: " + pointSystem.GetDefencePoints() + "/" + pointSystem.GetMaxDefencePoints();
        }

        private void RestoreHealth()
        {
            health = maxHealth;
        }

        private float GetHealth()
        {
            return health;
        }

        private float GetMaxHealth()
        {
            return maxHealth;
        }

        public void IncreaseMaxHealth()
        {
            health += 10;
            maxHealth += 10;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
            }
        }
    }
}
