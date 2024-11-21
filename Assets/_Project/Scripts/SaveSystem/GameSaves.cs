using System;
using System.Collections.Generic;

[Serializable]
public class GameSaves
{
    public PlayerStats PlayerStats { get; set; }
    public List<CharacterSaves> CharacterSaves { get; set; }
    public int CurrentCharacterIndex { get; set; }

    public void Save(PlayerStats playerStats, Shop shop)
    {
        PlayerStats = playerStats;
        CurrentCharacterIndex = shop.CurrentCharacterIndex;
        CharacterSaves = new();

        foreach (var character in shop.SellableCharacters)
        {
            CharacterSaves newCharacterSaves = new();
            newCharacterSaves.Save(character);
            CharacterSaves.Add(newCharacterSaves);
        }
    }

    public void Load(PlayerStats playerStats, Shop shop)
    {
        playerStats.LoadData(PlayerStats);
        LoadCharacterSaves(shop);
    }

    public void LoadCharacterSaves(Shop shop)
    {
        shop.CurrentCharacterIndex = CurrentCharacterIndex;

        foreach (var character in shop.SellableCharacters)
        {
            foreach (var characterSaves in CharacterSaves)
            {
                if (character.ID == characterSaves.ID)
                    characterSaves.Load(character);
            }
        }
    }
}