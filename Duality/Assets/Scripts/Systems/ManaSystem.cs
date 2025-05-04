using TMPro;
using Enemies;
using UnityEngine;

namespace Systems
{
    public class ManaSystem : MonoBehaviour
    {
        [Header("Mana Counter")] [SerializeField]
        private float currentMana;

        [SerializeField] private float maxMana;

        [SerializeField] private TextMeshProUGUI manaText;

        [Header("Mana Regeneration")] [SerializeField]
        private float manaRegenRate;

        [SerializeField] private EnemyBase enemyDrop;

        private void Start()
        {
            currentMana = 0;
            manaText.text = "Mana: " + currentMana + "/" + maxMana;
        }

        private void Update()
        {
            currentMana += manaRegenRate * Time.deltaTime;
            manaText.text = "Mana: " + currentMana + "/" + maxMana; // Limit to 1 or 2 dp
        }

        public void AddManaFromEnemy(GameObject enemy)
        {
            currentMana += enemy.GetComponent<EnemyBase>().GetManaDrop();
        }
    }
}