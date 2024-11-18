using UnityEngine;

public class ToBattleButton : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    private DataForLevel _dataForLevel;
    private Shop _shop;
    private PlayerStats _playerStats;

    public void Initialize(SceneLoader sceneLoader, DataForLevel dataForLevel, Shop shop, PlayerStats playerStats)
    {
        _sceneLoader = sceneLoader;
        _dataForLevel = dataForLevel;
        _shop = shop;
        _playerStats = playerStats;
    }

    public void OnClick()
    {
        var currentSellableCharacter = _shop.CurrentCharacter;
        var currentCharacterPrefab = currentSellableCharacter.Data.Prefab;
        _dataForLevel.PlayerCharacterPrefab = currentCharacterPrefab;
        _dataForLevel.PlayerDamage = currentSellableCharacter.DamageUpgrade.CurrentValue;
        _dataForLevel.PlayerHealth = currentSellableCharacter.HealthUpgrade.CurrentValue;
        _dataForLevel.PlayerFiringRate = currentSellableCharacter.FiringRateUpgrade.CurrentValue;
        Debug.Log($"Damage: {_dataForLevel.PlayerDamage}, " +
            $"Health: {_dataForLevel.PlayerHealth}, FiringRate: {_dataForLevel.PlayerFiringRate}");
        _sceneLoader.Load(_sceneLoader.LevelScene);
    }
}
