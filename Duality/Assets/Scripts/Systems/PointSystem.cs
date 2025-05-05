using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class PointSystem : MonoBehaviour
    {
        [Header("Attack")] [Tooltip("Current collected attack points")] [SerializeField]
        private int attackPoints;

        [Tooltip("Attack point limit")] [SerializeField]
        private int maxAttackPoints;

        [Header("Defence")] [Tooltip("Current collected defence points")] [SerializeField]
        private int defencePoints;

        [Tooltip("Defence point limit")] [SerializeField]
        private int maxDefencePoints;
        
        [Header("References")] [Tooltip("Reference to pellet controller to adjust attack stats")] [SerializeField]
        private PelletController pelletController;
        
        [Tooltip("Reference to player controller to adjust defence stats")] [SerializeField]
        private PlayerController playerController;
        
        [Tooltip("Reference to mana system to perform transactions")] [SerializeField]
        private ManaSystem manaSystem;

        public int GetAttackPoints()
        {
            return attackPoints;
        }

        public int GetMaxAttackPoints()
        {
            return maxAttackPoints;
        }

        public void AddAttackPoint(AttackUpgradeType type, float increment, GameObject card)
        {
            string costText = card.GetComponentsInChildren<TextMeshProUGUI>()[1].text.Split(" ")[0];
            double cost = double.Parse(costText);
            double diff = manaSystem.GetCurrentMana() - cost;
            if (attackPoints < maxAttackPoints && diff > 0)
            {
                attackPoints++;
                switch (type)
                {
                    case AttackUpgradeType.FireRate:
                        pelletController.IncreaseFireRate(increment);
                        break;
                    case AttackUpgradeType.PelletForce:
                        pelletController.IncreasePelletForce(increment);
                        break;
                }
                manaSystem.PurchaseWithMana(cost);
                card.SetActive(false);
            }
        }

        public void SubtractAttackPoint()
        {
            if (attackPoints > 0 && defencePoints < maxDefencePoints)
            {
                attackPoints--;
                //AddDefencePoint();
            }
            else
            {
                Debug.Log("AP=0 / DP=max");
            }
        }



        public int GetDefencePoints()
        {
            return defencePoints;
        }

        public int GetMaxDefencePoints()
        {
            return maxDefencePoints;
        }

        public void AddDefencePoint(DefenceUpgradeType type, float increment, GameObject card)
        {
            string costText = card.GetComponentsInChildren<TextMeshProUGUI>()[1].text.Split(" ")[0];
            double cost = double.Parse(costText);
            double diff = manaSystem.GetCurrentMana() - cost;
            if (defencePoints < maxDefencePoints && diff > 0)
            {
                defencePoints++;
                switch (type)
                {
                    case DefenceUpgradeType.MaxHealth:
                        playerController.IncreaseMaxHealth((int) increment);
                        break;
                    case DefenceUpgradeType.RangeSize:
                        pelletController.IncreaseRange(increment);
                        break;
                }
                manaSystem.PurchaseWithMana(cost);
                card.SetActive(false);
            }
        }

        public void SubtractDefencePoint()
        {
            if (defencePoints > 0 && attackPoints < maxAttackPoints)
            {
                defencePoints--;
                //AddAttackPoint();
            }
            else
            {
                Debug.Log("DP=0 / AP=max");
            }
        }
    }
}