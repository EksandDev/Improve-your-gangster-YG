using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform _environment;
    [SerializeField] private Transform _firstLevelPartSpawnPoint;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform _runDirectionPoint;
    [SerializeField] private Transform _runCameraPoint;

    [Header("Enemy things")]
    [SerializeField] private EnemyTrigger[] _enemyTriggers;
    [SerializeField] private EnemyView[] _enemies;

    [Header("Other")]
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHealth;

    private Level _level;
    private LevelMover _levelMover;
    private CameraController _cameraController;
    private PlayerCharacterView _playerView;
    private EnemyObjectPool _enemyObjectPool;
    private LevelPartObjectPool _levelPartObjectPool;
    private DataForLevel _dataForLevel;

    #region Zenject initialization
    [Inject]
    private void Construct(LevelMover levelMover, CameraController cameraController,
        EnemyObjectPool enemyObjectPool, LevelPartObjectPool levelPartObjectPool, DataForLevel dataForLevel)
    {
        _levelMover = levelMover;
        _cameraController = cameraController;
        _enemyObjectPool = enemyObjectPool;
        _levelPartObjectPool = levelPartObjectPool;
        _dataForLevel = dataForLevel;
    }
    #endregion

    private void Start()
    {
        _sceneContext.Run();
        _playerView = Instantiate(_dataForLevel.PlayerCharacterPrefab, _playerSpawnPoint.position,
            _playerSpawnPoint.rotation);
        _cameraController.Initialize(_runCameraPoint, _playerView.BattleCameraPoint);
        _level = new(_cameraController, _levelMover, _playerView);
        EnemyCreator enemyCreator = new(_level, _enemyObjectPool, _environment);
        LevelPartCreator levelPartCreator = new(_level, enemyCreator, _levelPartObjectPool, _environment);
        PlayerCharacterInitialize();
        _levelMover.Initialize();

        foreach (var enemyTrigger in _enemyTriggers)
            enemyTrigger.Initialize(_level);

        levelPartCreator.Create(_firstLevelPartSpawnPoint.position);
    }

    private void PlayerCharacterInitialize()
    {
        _playerView.Initialize
            (_enemyTriggers, _level, _dataForLevel.Damage, _dataForLevel.Health, _dataForLevel.FiringRate);
        _playerView.RunDirectionPoint = _runDirectionPoint;
    }
}
