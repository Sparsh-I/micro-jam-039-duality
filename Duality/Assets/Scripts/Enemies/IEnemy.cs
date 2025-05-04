namespace Enemies
{
    public interface IEnemy
    {
        void TakeDamage(float damage);
        float GetManaDrop();
        void SetAttackStat();
    }
}