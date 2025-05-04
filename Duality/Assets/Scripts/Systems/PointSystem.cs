using Managers;
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
        
        [Header("Stat Increments")] [Tooltip("How much the stats increase by with each card bought")] [SerializeField]
        private float statIncrement;
        
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

        public void AddAttackPoint()
        {
            double diff = manaSystem.GetCurrentMana();
            if (attackPoints < maxAttackPoints)
            {
                attackPoints++;
                pelletController.IncreaseFireRate(statIncrement);
            }
        }

        public void SubtractAttackPoint()
        {
            if (attackPoints > 0 && defencePoints < maxDefencePoints)
            {
                attackPoints--;
                AddDefencePoint();
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

        public void AddDefencePoint()
        {
            if (defencePoints < maxDefencePoints)
            {
                defencePoints++;
                playerController.IncreaseMaxHealth();
            }
        }

        public void SubtractDefencePoint()
        {
            if (defencePoints > 0 && attackPoints < maxAttackPoints)
            {
                defencePoints--;
                AddAttackPoint();
            }
            else
            {
                Debug.Log("DP=0 / AP=max");
            }
        }
    }
}