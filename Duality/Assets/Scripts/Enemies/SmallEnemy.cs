using Random = UnityEngine.Random;

namespace Enemies
{
    public class SmallEnemy : EnemyBase
    {
        public override float GetManaDrop()
        {
            return level * Random.Range(0.2f, 0.5f);
        }

        public override void SetAttackStat()
        {
            attackStat = 1 * (int)(level * level);
        }
    }
}