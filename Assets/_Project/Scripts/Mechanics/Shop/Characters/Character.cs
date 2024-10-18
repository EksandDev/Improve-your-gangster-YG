using UnityEngine;

public class Character : ISellable
{
    private CharacterData _data;
    private bool _isPurchased;

    public Upgrade DamageUpgrade { get; private set; }
    public Upgrade HealthUpgrade { get; private set; }
    public Upgrade FiringRateUpgrade { get; private set; }
    public GameObject InstantiatedPrefab {  get; private set; }
    public bool IsPurchased => _isPurchased;
    public int Cost => _data.Cost;

    public Character(CharacterData data, Transform characterSpawnPoint)
    {
        _data = data;
        InstantiatedPrefab = Object.Instantiate(_data.Prefab.gameObject, characterSpawnPoint.position, 
            characterSpawnPoint.rotation, characterSpawnPoint);
        InstantiatedPrefab.SetActive(false);
    }

    public void Buy()
    {
        _isPurchased = true;
    }
}