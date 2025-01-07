public class UpgradeSaves
{
    public int Cost { get; set; }
    public bool IsPurchased { get; set; }
    public int CurrentLevel { get; set; }

    public void Save(Upgrade upgrade)
    {
        Cost = upgrade.Cost;
        IsPurchased = upgrade.IsPurchased;
        CurrentLevel = upgrade.CurrentLevel;
    }

    public void Load(Upgrade upgrade) => upgrade.LoadData(this);
}