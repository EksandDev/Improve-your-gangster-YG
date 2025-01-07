using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : CreatorByCost<EnemyView>
{
    private Level _level;
    private DifficultCalculator _difficultCalculator;
    private LevelStatsCounter _levelStatsCounter;
    private Dictionary<EnemyView, EnemyObjectPool> _enemiesWithObjectPools;
    private List<int> _availableCosts;

    public IReadOnlyList<int> AvailableCosts => _availableCosts;

    public EnemyCreator(Level level, Transform productParent, DifficultCalculator difficultCalculator, 
        LevelStatsCounter levelStatsCounter)
    {
        _level = level;
        _difficultCalculator = difficultCalculator;
        _levelStatsCounter = levelStatsCounter;
        _availableCosts = new();
        _enemiesWithObjectPools = new();

        foreach (var availableEnemy in _difficultCalculator.AvailableEnemies)
        {
            var objectPool = availableEnemy.Data.ObjectPool;
            _enemiesWithObjectPools.Add(availableEnemy, objectPool);
            _availableCosts.Add(availableEnemy.Data.CostForSpawn);
            objectPool.Initialize(productParent);
        }
        _levelStatsCounter = levelStatsCounter;
    }

    public override EnemyView Create(Vector3 position, int cost)
    {
        EnemyView enemy = GetEnemyByCost(position, cost);

        if (enemy == null)
        {
            Debug.LogError("Enemy is null, spawning enemy with 0 cost");
            enemy = GetEnemyByCost(position, 0);
        }

        if (!enemy.IsInitialized)
        {
            var damage = enemy.Data.Damage * _difficultCalculator.EnemyDamageModifier;
            var maxHealth =  enemy.Data.MaxHealth * _difficultCalculator.EnemyHealthModifier;
            var firingRate = enemy.Data.FiringRate * _difficultCalculator.EnemyFiringRateModifier;
            enemy.Initialize(_level, damage, maxHealth, firingRate, enemy.Data.MoneyForKill);
            _level.Mover.AddMovingObject(enemy.GetComponent<MovableObject>());
        }

        _levelStatsCounter.SubscribeToDeath(enemy.Model);
        enemy.Model.RecoverHealth();

        return enemy;
    }

    private EnemyView GetEnemyByCost(Vector3 position, int cost)
    {
        foreach (var item in _enemiesWithObjectPools)
        {
            if (item.Key.Data.CostForSpawn != cost)
                continue;

            return item.Value.Create(position);
        }

        return null;
    }
}