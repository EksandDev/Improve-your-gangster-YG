public class PlayerStats
{
    public int Money { get; set; } = 100000;
    public int CurrentLevel { get; set; } = 3;

    public void LoadData(PlayerStats playerStats)
    {
        Money = playerStats.Money;
        CurrentLevel = playerStats.CurrentLevel;
    }
}
