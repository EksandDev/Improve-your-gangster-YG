using UnityEngine;

public class LevelPartCreator : Creator<LevelPart>
{
    private Level _level;
    private EnemyCreator _enemyCreator;
    private LevelPartObjectPool _objectPool;

    public LevelPartCreator(Level level, EnemyCreator enemyCreator,
        LevelPartObjectPool objectPool, Transform productParent)
    {
        _level = level;
        _enemyCreator = enemyCreator;
        _objectPool = objectPool;

        _objectPool.Initialize(productParent);
    }

    public override LevelPart Create(Vector3 position)
    {
        var levelPart = _objectPool.Create(position);

        if (!levelPart.IsInitialized)
        {
            levelPart.Initialize(_enemyCreator, this);
            _level.Mover.AddMovingObject(levelPart.GetComponent<MovableObject>());
        }

        levelPart.SpawnEnemies();

        return levelPart;
    }
}