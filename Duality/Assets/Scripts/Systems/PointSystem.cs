using UnityEngine;

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

        [Header("Reserve")] [Tooltip("Stored points when switching points around between rounds")] [SerializeField]
        private int reservePoints;

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
            if (attackPoints < maxAttackPoints) attackPoints++;
        }

        public void SubtractAttackPoint()
        {
            if (attackPoints > 0) attackPoints--;
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
            if (defencePoints < maxDefencePoints) defencePoints++;
        }

        public void SubtractDefencePoint()
        {
            if (defencePoints > 0) defencePoints--;
        }



        public int GetReservePoints()
        {
            return reservePoints;
        }

        public void AddReservePoint()
        {
            reservePoints++;
        }

        public bool SubtractReservePoint()
        {
            if (reservePoints > 0)
            {
                reservePoints--;
                return true;
            }

            return false;
        }
    }
}