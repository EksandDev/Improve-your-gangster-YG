using UnityEngine;

public class EnemyCreator : Creator<EnemyView>
{
    private Level _level;
    private EnemyObjectPool _objectPool;

    public EnemyCreator(Level level, EnemyObjectPool objectPool, Transform productParent)
    {
        _level = level;
        _objectPool = objectPool;

        _objectPool.Initialize(productParent);
    }

    public override EnemyView Create(Vector3 position)
    {
        var enemy = _objectPool.Create(position);

        if (!enemy.IsInitialized)
        {
            enemy.Initialize(_level);
            _level.Mover.AddMovingObject(enemy.GetComponent<MovingObject>());
        }

        enemy.Model.RecoverHealth();

        return enemy;
    }
}