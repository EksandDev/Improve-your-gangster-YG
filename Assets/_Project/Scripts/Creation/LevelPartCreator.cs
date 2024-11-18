using UnityEngine;

public class LevelPartCreator : Creator<LevelPart>
{
    private Level _level;
    private EnemyCreator _enemyCreator;
    private LevelPartObjectPool _objectPool;
    private FinalLevelPart _finalLevelPart;
    private CameraController _cameraController;
    private PlayerStats _playerStats;
    private DifficultCalculator _difficultCalculator;
    private Transform _finishPopup;
    private int _spawnedLevelPartsCount;

    private const int MAX_SPAWNED_LEVEL_PARTS_COUNT = 3;

    public LevelPartCreator(Level level, EnemyCreator enemyCreator, LevelPartObjectPool objectPool,
        FinalLevelPart finalLevelPart, CameraController cameraController, PlayerStats playerStats,
        DifficultCalculator difficultCalculator,Transform finishPopup, Transform productParent)
    {
        _level = level;
        _enemyCreator = enemyCreator;
        _objectPool = objectPool;
        _finalLevelPart = finalLevelPart;
        _cameraController = cameraController;
        _playerStats = playerStats;
        _difficultCalculator = difficultCalculator;
        _finishPopup = finishPopup;

        _objectPool.Initialize(productParent);
    }

    public override LevelPart Create(Vector3 position)
    {
        if (_spawnedLevelPartsCount >= MAX_SPAWNED_LEVEL_PARTS_COUNT)
        {
            var finalLevelPart = Object.Instantiate(_finalLevelPart, position, Quaternion.identity);
            finalLevelPart.FinishTrigger.Initialize(_cameraController, _playerStats, _finishPopup);
            _level.Mover.AddMovingObject(finalLevelPart.GetComponent<MovableObject>());
            return finalLevelPart;
        }

        var levelPart = _objectPool.Create(position);
        _spawnedLevelPartsCount++;

        if (!levelPart.IsInitialized)
        {
            levelPart.Initialize(_enemyCreator, this);
            _level.Mover.AddMovingObject(levelPart.GetComponent<MovableObject>());
        }

        levelPart.SpawnEnemies(_difficultCalculator.EnemyPoints);

        return levelPart;
    }
}