using ModestTree;
using System;
using System.Collections.Generic;

public class Shop : ISaveCaller
{
    private List<Character> _sellableCharacters;
    private PlayerStats _playerStats;
    private UpgradeUIController _upgradeUIController;
    private int _currentCharacterIndex;

    public event Action CallingSave;

    public IReadOnlyList<Character> SellableCharacters => _sellableCharacters;
    public Character CurrentCharacter => _sellableCharacters[CurrentCharacterIndex];
    public int CurrentCharacterIndex
    {
        get => _currentCharacterIndex;
        set
        {
            if (value == _currentCharacterIndex || value < 0 || value > _sellableCharacters.Count - 1)
                return;

            CurrentCharacter.InstantiatedPrefab.gameObject.SetActive(false);
            _currentCharacterIndex = value;
            CurrentCharacter.InstantiatedPrefab.gameObject.SetActive(true);
            _upgradeUIController.CurrentCharacter = CurrentCharacter;
        }
    }

    public Shop(PlayerStats playerStats, List<Character> sellableCharacters, 
        UpgradeUIController upgradeUIController)
    {
        if (sellableCharacters == null || sellableCharacters.Count == 0)
        {
            Log.Error("Sellable characters is null!");
            return;
        }

        _sellableCharacters = sellableCharacters;
        _playerStats = playerStats;
        _upgradeUIController = upgradeUIController;
        _upgradeUIController.CurrentCharacter = CurrentCharacter;
    }

    public bool TryBuyItem(ISellable item)
    {
        if (_playerStats.Money < item.Cost || item.IsPurchased)
            return false;

        item.Buy();
        _playerStats.Money -= item.Cost;
        CallingSave?.Invoke();

        return true;
    }
}