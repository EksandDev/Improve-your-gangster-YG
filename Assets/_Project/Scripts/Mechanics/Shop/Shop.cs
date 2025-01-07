using ModestTree;
using System;
using System.Collections.Generic;

public class Shop : ISaveCaller
{
    private List<Character> _sellableCharacters;
    private PlayerStats _playerStats;
    private UpgradeUIController _upgradeUIController;
    private CharacterPurchaseButton _characterPurchaseButton;
    private int _currentCharacterIndex;

    public event Action CallingSave;
    public event Action ItemPurchased;

    public IReadOnlyList<Character> SellableCharacters => _sellableCharacters;
    public Character CurrentCharacter => _sellableCharacters[CurrentCharacterIndex];
    public int CurrentCharacterIndex
    {
        get => _currentCharacterIndex;
        set
        {
            if (value == _currentCharacterIndex || value < 0 || value > _sellableCharacters.Count - 1)
                return;

            UpdateCharacter(value);
        }
    }

    public Shop(PlayerStats playerStats, List<Character> sellableCharacters,
        UpgradeUIController upgradeUIController, CharacterPurchaseButton characterPurchaseButton)
    {
        if (sellableCharacters == null || sellableCharacters.Count == 0)
        {
            Log.Error("Sellable characters is null!");
            return;
        }

        _sellableCharacters = sellableCharacters;
        _playerStats = playerStats;
        _upgradeUIController = upgradeUIController;
        _characterPurchaseButton = characterPurchaseButton;
        UpdateCharacter(CurrentCharacterIndex);
        _upgradeUIController.CurrentCharacter = CurrentCharacter;
    }

    public bool TryBuyItem(ISellable item)
    {
        if (_playerStats.Money < item.Cost || item.IsPurchased)
            return false;

        _playerStats.Money -= item.Cost;
        item.Buy();
        ItemPurchased?.Invoke();
        CallingSave?.Invoke();
        UpdateUI();

        return true;
    }

    public void UpdateUI()
    {
        _upgradeUIController.CurrentCharacter = CurrentCharacter;
        _upgradeUIController.SetActiveUpgrades(CurrentCharacter.IsPurchased);
        _characterPurchaseButton.SetActive(!CurrentCharacter.IsPurchased);
        _characterPurchaseButton.Item = CurrentCharacter;
        _characterPurchaseButton.Cost = CurrentCharacter.Cost;
    }

    private void UpdateCharacter(int newCharacterIndex)
    {
        CurrentCharacter.InstantiatedPrefab.gameObject.SetActive(false);
        _currentCharacterIndex = newCharacterIndex;
        CurrentCharacter.InstantiatedPrefab.gameObject.SetActive(true);
        UpdateUI();
    }
}