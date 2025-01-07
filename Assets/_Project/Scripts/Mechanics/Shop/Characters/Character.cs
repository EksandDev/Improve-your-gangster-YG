using UnityEngine;

public class Character : ISellable
{
    private CharacterData _data;
    private bool _isPurchased;

    public StatsUpgrade DamageUpgrade { get; private set; }
    public StatsUpgrade HealthUpgrade { get; private set; }
    public StatsUpgrade FiringRateUpgrade { get; private set; }
    public PlayerCharacterView InstantiatedPrefab {  get; private set; }
    public CharacterData Data => _data;
    public bool IsPurchased => _isPurchased;
    public int Cost => _data.Cost;
    public int ID => _data.ID;

    public Character(CharacterData data, Transform characterSpawnPoint)
    {
        _data = data;
        DamageUpgrade = new(_data.UpgradeCostUp, 
            _data.DamageUpgradeMaxLevel, _data.UpgradeCostUp, _data.DamageUpgradeLevelValues);
        FiringRateUpgrade = new(_data.UpgradeCostUp, 
            _data.FiringRateUpgradeMaxLevel, _data.UpgradeCostUp, _data.FiringRateUpgradeLevelValues);
        HealthUpgrade = new (_data.UpgradeCostUp, 
            _data.HealthUpgradeMaxLevel, _data.UpgradeCostUp, _data.HealthUpgradeLevelValues);
        InstantiatedPrefab = Object.Instantiate(_data.Prefab, characterSpawnPoint.position, 
            characterSpawnPoint.rotation, characterSpawnPoint);
        InstantiatedPrefab.gameObject.SetActive(false);
        _isPurchased = data.IsPurchasedOnStart;
    }

    public void Buy() => _isPurchased = true;

    public void LoadData(CharacterSaves saves) => _isPurchased = saves.IsPurchased;
}