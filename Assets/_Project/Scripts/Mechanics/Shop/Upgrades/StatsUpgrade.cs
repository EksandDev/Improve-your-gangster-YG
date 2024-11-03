public class StatsUpgrade : Upgrade
{
    public float[] LevelValues { get; }
    public float CurrentValue => LevelValues[CurrentLevel];

    public StatsUpgrade(int cost, int maxLevel, float[] levelValues) : base(cost, maxLevel)
    {
        LevelValues = levelValues;
    }

    public override void Buy()
    {
        ChangeCurrentLevel(CurrentLevel + 1);

        if (CurrentLevel >= MaxLevel)
        {
            ChangePurchaseStatus(true);
            return;
        }
    }
}