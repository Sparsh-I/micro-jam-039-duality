using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Health")] [Tooltip("Current player health")] [SerializeField]
        private float health;

        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;
        
        [Tooltip("Max player health")] [SerializeField]
        private float maxHealth;

        [Header("Points")] [SerializeField] private PointSystem pointSystem;

        [SerializeField] private TextMeshProUGUI attackPointsText;

        [SerializeField] private TextMeshProUGUI defencePointsText;

        void Start()
        {
            slider.maxValue = maxHealth;
            health = maxHealth;
            attackPointsText.text = "0";;
            defencePointsText.text = "0";
            
            fill.color = gradient.Evaluate(1f);
        }

        void Update()
        {
            slider.value = GetHealth();
            slider.maxValue = GetMaxHealth();
            
            fill.color = gradient.Evaluate(slider.normalizedValue);
            
            attackPointsText.text = pointSystem.GetAttackPoints().ToString();
            defencePointsText.text = pointSystem.GetDefencePoints().ToString();
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

        public void IncreaseMaxHealth(int increment)
        {
            health += increment;
            maxHealth += increment;

            fill.color = gradient.Evaluate(1f);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                SceneManager.LoadScene("Game Over");
            }
        }
    }
}
