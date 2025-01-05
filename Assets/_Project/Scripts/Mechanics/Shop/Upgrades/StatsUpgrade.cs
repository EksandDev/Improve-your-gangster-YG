public class StatsUpgrade : Upgrade
{
    public int CostUp { get; }
    public float[] LevelValues { get; }
    public float CurrentValue => LevelValues[CurrentLevel];

    public StatsUpgrade(int cost, int maxLevel, int costUp, float[] levelValues) : base(cost, maxLevel)
    {
        LevelValues = levelValues;
        CostUp = costUp;
    }

    public override void Buy()
    {
        ChangeCost(Cost + CostUp);
        ChangeCurrentLevel(CurrentLevel + 1);

        if (CurrentLevel >= MaxLevel)
        {
            ChangePurchaseStatus(true);
            return;
        }
    }
}