public class UpgradeSaves
{
    public bool IsPurchased { get; set; }
    public int CurrentLevel { get; set; }

    public void Save(Upgrade upgrade)
    {
        IsPurchased = upgrade.IsPurchased;
        CurrentLevel = upgrade.CurrentLevel;
    }

    public void Load(Upgrade upgrade) => upgrade.LoadData(this);
}