using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private PlayerStats _playerStats;
    private Shop _shop;

    private readonly string _path = Application.persistentDataPath + "/GameSaves.json";

    public GameSaves CurrentGameSaves { get; private set; }

    public SaveSystem(PlayerStats playerStats, Shop shop, List<ISaveCaller> saveCallers)
    {
        _playerStats = playerStats;
        _shop = shop;

        foreach (var caller in saveCallers)
            caller.CallingSave += Save;
    }

    public void Save()
    {
        try
        {
            if (File.Exists(_path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one");
                File.Delete(_path);
            }

            else
                Debug.Log("Writing file for the first time");

            CurrentGameSaves = new();
            CurrentGameSaves.Save(_playerStats, _shop);
            using FileStream stream = File.Create(_path);
            stream.Close();
            var serializedGameSaves = JsonConvert.SerializeObject(CurrentGameSaves);
            File.WriteAllText(_path, serializedGameSaves);
            Debug.Log($"Saving JSON:{serializedGameSaves}");
        }

        catch (Exception exception)
        {
            Debug.LogError($"Unable to save data due to: {exception.Message} {exception.StackTrace}");
            throw exception;
        }
    }

    public GameSaves Load()
    {
        try
        {
            if (!File.Exists(_path))
                Debug.Log($"Cannot load file at {_path}");

            var deserializedGameSaves = JsonConvert.DeserializeObject<GameSaves>(File.ReadAllText(_path));
            Debug.Log($"Loading JSON: {File.ReadAllText(_path)}");
            return deserializedGameSaves;
        }

        catch (Exception exception)
        {
            Debug.Log($"Failed to load data due to: {exception.Message} {exception.StackTrace}");
            return null;
        }
    }
}