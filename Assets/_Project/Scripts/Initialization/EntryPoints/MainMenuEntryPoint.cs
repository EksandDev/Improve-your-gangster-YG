using System.Collections.Generic;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private ChangeCharacterButton[] _changeCharacterButtons;

    [Header("Characters")]
    [SerializeField] private CharacterData[] _characterData;
    [SerializeField] private Transform _characterSpawnPoint;

    private Shop _shop;
    private PlayerStats _playerStats;
    private List<Character> _sellableCharacters;

    private void Start()
    {
        _playerStats = new();
        InitializeCharacters();
        _shop = new(_playerStats, _sellableCharacters);
        InitializeButtons();

        for (int i = 0; i < _sellableCharacters.Count - 1; i++ )
        {
            if (i != _shop.CurrentCharacterIndex)
                continue;

            _sellableCharacters[i].InstantiatedPrefab.SetActive(true);
            break;
        }
    }

    private void InitializeCharacters()
    {
        _sellableCharacters = new();

        foreach (var characterData in _characterData)
        {
            Character character = new(characterData, _characterSpawnPoint);
            _sellableCharacters.Add(character);
        }
    }

    private void InitializeButtons()
    {
        foreach (var button in _changeCharacterButtons)
            button.Inititalize(_shop);
    }
}
