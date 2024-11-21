public class CharacterSaves
{
    public UpgradeSaves DamageUpgradeSaves { get; set; }
    public UpgradeSaves HealthUpgradeSaves { get; set; }
    public UpgradeSaves FiringRateUpgradeSaves { get; set; }
    public bool IsPurchased { get; set; }
    public int ID { get; set; }

    public void Save(Character character)
    {
        IsPurchased = character.IsPurchased;
        ID = character.ID;
        DamageUpgradeSaves = new();
        HealthUpgradeSaves = new();
        FiringRateUpgradeSaves = new();
        DamageUpgradeSaves.Save(character.DamageUpgrade);
        HealthUpgradeSaves.Save(character.HealthUpgrade);
        FiringRateUpgradeSaves.Save(character.FiringRateUpgrade);
    }

    public void Load(Character character)
    {
        character.LoadData(this);
        DamageUpgradeSaves.Load(character.DamageUpgrade);
        HealthUpgradeSaves.Load(character.HealthUpgrade);
        FiringRateUpgradeSaves.Load(character.FiringRateUpgrade);
    }
}
