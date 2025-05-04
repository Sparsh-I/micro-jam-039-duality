using Random = UnityEngine.Random;

namespace Enemies
{
    public class MediumEnemy : EnemyBase
    {
        public override float GetManaDrop()
        {
            return level * Random.Range(0.6f, 1f);
        }

        public override void SetAttackStat()
        {
            attackStat = 3 * (int)(level * level);
        }
    }
}