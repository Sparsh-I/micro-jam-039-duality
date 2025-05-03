using UnityEngine;

public class PointController : MonoBehaviour
{
    [Header("Attack")]
    [Tooltip("Current collected attack points")]
    [SerializeField] private int attackPoints;
    
    [Tooltip("Attack point limit")]
    [SerializeField] private int maxAttackPoints;
    
    [Header("Defence")]
    [Tooltip("Current collected defence points")]
    [SerializeField] private int defencePoints;
    
    [Tooltip("Defence point limit")]
    [SerializeField] private int maxDefencePoints;
    
    [Header("Reserve")]
    [Tooltip("Stored points when switching points around between rounds")]
    [SerializeField] private int reservePoints;
    
    public float GetAttackPoints()
    {
        return attackPoints;
    }

    private int AddAttackPoint()
    {
        if (attackPoints < maxAttackPoints && SubtractReservePoint()) attackPoints++;
        return attackPoints;
    }

    private int SubtractAttackPoint()
    {
        if (attackPoints > 0) attackPoints--;
        return attackPoints;
    }

    public int GetDefencePoints()
    {
        return defencePoints;
    }
    
    private int AddDefencePoint()
    {
        if (defencePoints < maxDefencePoints && SubtractReservePoint()) defencePoints++;
        return defencePoints;
    }

    private int SubtractDefencePoint()
    {
        if (defencePoints > 0) defencePoints--;
        return defencePoints;
    }

    public int GetReservePoints()
    {
        return reservePoints;
    }

    private int AddReservePoint()
    {
        reservePoints++;
        return reservePoints;
    }

    private bool SubtractReservePoint()
    {
        if (reservePoints > 0)
        {
            reservePoints--;
            return true;
        }

        return false;
    }
}