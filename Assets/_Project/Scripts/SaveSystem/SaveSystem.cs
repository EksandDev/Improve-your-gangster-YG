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
    private readonly string _gameSavesKey = "GameSaves";

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
            CurrentGameSaves = new();
            CurrentGameSaves.Save(_playerStats, _shop);
            var serializedGameSaves = JsonConvert.SerializeObject(CurrentGameSaves);
            Debug.Log($"Saving JSON:{serializedGameSaves}");

#if UNITY_WEBGL
            PlayerPrefs.SetString(_gameSavesKey, serializedGameSaves);
            return;
#endif
#pragma warning disable CS0162
            if (File.Exists(_path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one");
                File.Delete(_path);
            }

            else
                Debug.Log("Writing file for the first time");

            using FileStream stream = File.Create(_path);
            stream.Close();
            File.WriteAllText(_path, serializedGameSaves);
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
            string serializedGameSaves = null;

#if UNITY_WEBGL
            serializedGameSaves = PlayerPrefs.GetString(_gameSavesKey);
            Debug.Log($"Loading JSON: {File.ReadAllText(serializedGameSaves)}");
            return JsonConvert.DeserializeObject<GameSaves>(serializedGameSaves);
#endif
#pragma warning disable CS0162
            if (!File.Exists(_path))
                Debug.Log($"Cannot load file at {_path}");

            serializedGameSaves = File.ReadAllText(_path);
            Debug.Log($"Loading JSON: {serializedGameSaves}");
            return JsonConvert.DeserializeObject<GameSaves>(serializedGameSaves);
        }

        catch (Exception exception)
        {
            Debug.Log($"Failed to load data due to: {exception.Message} {exception.StackTrace}");
            return null;
        }
    }
}