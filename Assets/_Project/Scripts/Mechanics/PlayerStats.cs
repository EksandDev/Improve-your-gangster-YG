public class PlayerStats
{
    public int Money { get; set; } = 0;
    public int CurrentLevel { get; set; } = 1;

    public void LoadData(PlayerStats playerStats)
    {
        Money = playerStats.Money;
        CurrentLevel = playerStats.CurrentLevel;
    }
}
