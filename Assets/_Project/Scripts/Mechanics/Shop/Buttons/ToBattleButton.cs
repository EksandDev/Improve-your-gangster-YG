using UnityEngine;

public class ToBattleButton : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    private DataForLevel _dataForLevel;
    private Shop _shop;

    private const string LEVEL_SCENE = "DevScene";

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
        _dataForLevel.Damage = currentSellableCharacter.DamageUpgrade.CurrentValue;
        _dataForLevel.Health = currentSellableCharacter.HealthUpgrade.CurrentValue;
        _dataForLevel.FiringRate = currentSellableCharacter.FiringRateUpgrade.CurrentValue;
        Debug.Log($"Damage: {_dataForLevel.Damage}, " +
            $"Health: {_dataForLevel.Health}, FiringRate: {_dataForLevel.FiringRate}");
        _sceneLoader.Load(LEVEL_SCENE);
    }
}
