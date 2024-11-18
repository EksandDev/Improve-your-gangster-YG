using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovableObject))]
public class LevelPart : MonoBehaviour, IProduct
{
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform _nextLevelPartSpawnPoint;

    private EnemyCreator _enemyCreator;
    private List<EnemyView> _enemies;
    private bool _isInitialized;

    public bool IsInitialized => _isInitialized;
    public Transform NextLevelPartSpawnPoint => _nextLevelPartSpawnPoint;
    public LevelPartCreator LevelPartCreator { get; private set; }

    public void Initialize(EnemyCreator enemyCreator, LevelPartCreator levelPartCreator)
    {
        _isInitialized = true;
        _enemyCreator = enemyCreator;
        LevelPartCreator = levelPartCreator;
    }

    public void SpawnEnemies(int enemyPoints)
    {
        if (_enemies != null)
        {
            foreach (var enemy in _enemies)
                enemy.gameObject.SetActive(false);
        }

        _enemies = new();

        foreach (var spawnPoint in _enemySpawnPoints)
        {
            List<int> availableCosts = new();

            foreach (var availableCost in _enemyCreator.AvailableCosts)
            {
                if (availableCost > enemyPoints || availableCost == 0)
                    continue;

                availableCosts.Add(availableCost);
            }

            int costForSpawn;

            if (availableCosts.Count == 0 || enemyPoints == 0)
            {
                _enemies.Add(_enemyCreator.Create(spawnPoint.position, 0));
                continue;
            }

            costForSpawn = availableCosts[Random.Range(0, availableCosts.Count)];
            _enemies.Add(_enemyCreator.Create(spawnPoint.position, costForSpawn));
            enemyPoints -= costForSpawn;
        }
    }

    public void Deactivate()
    {
        foreach (var enemy in _enemies)
            enemy.gameObject.SetActive(false);

        _enemies = null;

        gameObject.SetActive(false);
    }
}