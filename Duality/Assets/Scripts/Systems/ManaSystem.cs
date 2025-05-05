using System;
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

        [SerializeField] private bool isInShop;

        private void Start()
        {
            currentMana = 0;
            manaText.text = "Mana: " + currentMana;
            StartManaTimer();
        }

        private void Update()
        {
            if (!isInShop)
            {
                currentMana += manaRegenRate * Time.deltaTime;
                if (currentMana >= maxMana) currentMana = maxMana;
            }
            manaText.text = "Mana: " + Math.Round(currentMana, 1);
        }

        public void StartManaTimer()
        {
            isInShop = false;
        }

        public void StopManaTimer()
        {
            isInShop = true;
        }

        public void AddManaFromEnemy(GameObject enemy)
        {
            currentMana += enemy.GetComponent<EnemyBase>().GetManaDrop();
        }

        public void PurchaseWithMana(double mana)
        {
            currentMana -= (float) mana;
        }

        public double GetCurrentMana()
        {
            return Math.Round(currentMana, 1);
        }
    }
}