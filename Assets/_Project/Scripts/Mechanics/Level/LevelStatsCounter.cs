public class LevelStatsCounter
{
    public int EarnedMoney { get; private set; }
    public int KilledEnemies { get; private set; }

    public void SubscribeToDeath(EnemyModel enemy)
    {
        enemy.OverrideDied += OnDeath;
    }

    private void OnDeath(EnemyModel enemy)
    {
        EarnedMoney += enemy.MoneyForKill;
        KilledEnemies++;
        enemy.OverrideDied -= OnDeath;
    }
}