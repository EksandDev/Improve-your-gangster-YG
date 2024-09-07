using UnityEngine;

public class EnemyCreator : Creator<EnemyView>
{
    private EnemyView[] _prefabs;
    private Level _level;

    public EnemyCreator(EnemyView[] prefabs, Level level)
    {
        _prefabs = prefabs;
        _level = level;
    }

    public override EnemyView Create(Vector3 position, Transform parent = null)
    {
        var enemy = Object.Instantiate(_prefabs[0], position, Quaternion.identity, parent);
        enemy.Initialize(_level);
        return enemy;
    }
}