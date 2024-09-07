using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour, IProduct
{
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform _nextLevelPartSpawnPoint;

    private List<EnemyView> _enemies;

    public void Initialize(EnemyCreator enemyCreator)
    {
        foreach (var spawnPoint in _enemySpawnPoints)
        {
            _enemies.Add(enemyCreator.Create(spawnPoint.position, transform.parent));
        }
    }
}