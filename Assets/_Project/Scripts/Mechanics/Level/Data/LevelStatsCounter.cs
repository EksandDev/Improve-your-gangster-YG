public class LevelStatsCounter
{
    public int EarnedMoney { get; private set; }
    public int KilledEnemies { get; private set; }

    private SmoothGameProcessSlider _gameProcessGameProcessSlider;

    public LevelStatsCounter(SmoothGameProcessSlider gameProcessGameProcessSlider)
    {
        _gameProcessGameProcessSlider = gameProcessGameProcessSlider;
        _gameProcessGameProcessSlider.SetValue(0);
    }
    
    public void SubscribeToDeath(EnemyModel enemy)
    {
        enemy.OverrideDied += OnDeath;
    }

    private void OnDeath(EnemyModel enemy)
    {
        EarnedMoney += enemy.MoneyForKill;
        KilledEnemies++;
        enemy.OverrideDied -= OnDeath;
        _gameProcessGameProcessSlider.SetValue(KilledEnemies);

        if (KilledEnemies == 9)
            EarnedMoney += 20;
    }
}