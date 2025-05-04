using Random = UnityEngine.Random;

namespace Enemies
{
    public class LargeEnemy : EnemyBase
    {
        public override float GetManaDrop()
        {
            return level * Random.Range(1.1f, 1.7f);
        }

        public override void SetAttackStat()
        {
            attackStat = 7 * (int)(level * level);
        }
    }
}