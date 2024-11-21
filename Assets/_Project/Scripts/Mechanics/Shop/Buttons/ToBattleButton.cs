using System;
using UnityEngine;

public class ToBattleButton : MonoBehaviour, ISaveCaller
{
    private SceneLoader _sceneLoader;
    private DataForLevel _dataForLevel;
    private Shop _shop;

    public event Action CallingSave;

    public void Initialize(SceneLoader sceneLoader, DataForLevel dataForLevel, Shop shop)
    {
        _sceneLoader = sceneLoader;
        _dataForLevel = dataForLevel;
        _shop = shop;
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
        CallingSave?.Invoke();
        _sceneLoader.Load(_sceneLoader.LevelScene);
    }
}
