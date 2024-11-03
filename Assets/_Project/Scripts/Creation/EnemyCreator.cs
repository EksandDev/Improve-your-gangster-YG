using UnityEngine;

public class EnemyCreator : Creator<EnemyView>
{
    private Level _level;
    private EnemyObjectPool _objectPool;
    private DifficultCalculator _difficultCalculator;

    public EnemyCreator(Level level, EnemyObjectPool objectPool, 
        Transform productParent, DifficultCalculator difficultCalculator)
    {
        _level = level;
        _objectPool = objectPool;
        _difficultCalculator = difficultCalculator;

        _objectPool.Initialize(productParent);
    }

    public override EnemyView Create(Vector3 position)
    {
        var enemy = _objectPool.Create(position);

        if (!enemy.IsInitialized)
        {
            var damage = enemy.Data.Damage * _difficultCalculator.EnemyDamageModifier;
            var maxHealth =  enemy.Data.MaxHealth * _difficultCalculator.EnemyHealthModifier;
            var firingRate = enemy.Data.FiringRate * _difficultCalculator.EnemyFiringRateModifier;
            enemy.Initialize(_level, damage, maxHealth, firingRate);
            _level.Mover.AddMovingObject(enemy.GetComponent<MovableObject>());
        }

        enemy.Model.RecoverHealth();

        return enemy;
    }
}