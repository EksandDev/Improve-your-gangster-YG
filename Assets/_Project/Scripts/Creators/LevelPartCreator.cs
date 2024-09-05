using UnityEngine;

public class LevelPartCreator : Creator<LevelPart>
{
    private LevelPart[] _prefabs;
    private Level _level;

    public LevelPartCreator(LevelPart[] prefabs, Level level)
    {
        _prefabs = prefabs;
        _level = level;
    }

    public override LevelPart Create(Vector3 position, Transform parent = null)
    {
        var levelPart = Object.Instantiate(_prefabs[0], position, Quaternion.identity, parent);
        //levelPart.Initialize(_level, _data.Damage, _data.MaxHealth);
        return levelPart;
    }
}