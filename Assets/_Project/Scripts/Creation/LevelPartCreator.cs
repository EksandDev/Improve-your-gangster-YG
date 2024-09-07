using UnityEngine;

public class LevelPartCreator : Creator<LevelPart>
{
    private LevelPart[] _prefabs;
    private Level _level;
    private EnemyCreator _enemyCreator;

    public LevelPartCreator(LevelPart[] prefabs, Level level, EnemyCreator enemyCreator)
    {
        _prefabs = prefabs;
        _level = level;
        _enemyCreator = enemyCreator;
    }

    public override LevelPart Create(Vector3 position, Transform parent = null)
    {
        var levelPart = Object.Instantiate(_prefabs[0], position, Quaternion.identity, parent);
        levelPart.Initialize(_enemyCreator);
        return levelPart;
    }
}