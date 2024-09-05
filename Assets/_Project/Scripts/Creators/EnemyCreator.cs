using UnityEngine;

public class EnemyCreator : Creator<EnemyView>
{
    private EnemyView[] _prefabs;
    private Level _level;
    private EnemyData _data;

    public EnemyCreator(EnemyView[] prefabs, Level level, EnemyData data)
    {
        _prefabs = prefabs;
        _level = level;
        _data = data;
    }

    public override EnemyView Create(Vector3 position, Transform parent = null)
    {
        var enemy = Object.Instantiate(_prefabs[0], position, Quaternion.identity, parent);
        enemy.Initialize(_level, _data.Damage, _data.MaxHealth);
        return enemy;
    }
}