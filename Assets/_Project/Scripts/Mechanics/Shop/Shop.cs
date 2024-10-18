using ModestTree;
using System.Collections.Generic;

public class Shop
{
    private List<Character> _sellableCharacters;
    private PlayerStats _playerStats;
    private int _currentCharacterIndex;

    public IReadOnlyList<Character> SellableCharacters => _sellableCharacters;
    public int CurrentCharacterIndex
    {
        get => _currentCharacterIndex;
        set
        {
            if (value == _currentCharacterIndex || value < 0 || value > _sellableCharacters.Count - 1)
                return;

            _sellableCharacters[_currentCharacterIndex].InstantiatedPrefab.SetActive(false);
            _currentCharacterIndex = value;
            _sellableCharacters[_currentCharacterIndex].InstantiatedPrefab.SetActive(true);
        }
    }

    public Shop(PlayerStats playerStats, List<Character> sellableCharacters)
    {
        if (sellableCharacters == null || sellableCharacters.Count == 0)
        {
            Log.Error("Sellable characters is null!");
            return;
        }

        _sellableCharacters = sellableCharacters;
        _playerStats = playerStats;
    }

    public bool TryBuyItem(ISellable item)
    {
        if (_playerStats.Money < item.Cost || item.IsPurchased)
            return false;

        item.Buy();
        _playerStats.Money -= item.Cost;

        return true;
    }
}