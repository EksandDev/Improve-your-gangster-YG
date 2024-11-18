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

    [Header("UI")]
    [SerializeField] private Transform _failurePopup;
    [SerializeField] private Transform _finishPopup;
    [SerializeField] private ToShopButton[] _toShopButtons;

    [Header("Enemy things")]
    [SerializeField] private EnemyView[] _enemyPrefabs;
    [SerializeField] private EnemyTrigger[] _enemyTriggers;

    [Header("Other")]
    [SerializeField] private FinalLevelPart _finalLevelPart;
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHealth;

    private Level _level;
    private LevelMover _levelMover;
    private CameraController _cameraController;
    private SceneLoader _sceneLoader;
    private PlayerCharacterView _playerView;
    private EnemyObjectPool _enemyObjectPool;
    private LevelPartObjectPool _levelPartObjectPool;
    private DataForLevel _dataForLevel;
    private PlayerStats _playerStats;

    #region Zenject initialization
    [Inject]
    private void Construct(LevelMover levelMover, CameraController cameraController, SceneLoader sceneLoader,
        EnemyObjectPool enemyObjectPool, LevelPartObjectPool levelPartObjectPool, DataForLevel dataForLevel,
        PlayerStats playerStats)
    {
        _levelMover = levelMover;
        _cameraController = cameraController;
        _sceneLoader = sceneLoader;
        _enemyObjectPool = enemyObjectPool;
        _levelPartObjectPool = levelPartObjectPool;
        _dataForLevel = dataForLevel;
        _playerStats = playerStats;
    }
    #endregion

    private void Start()
    {
        _sceneContext.Run();
        _playerView = Instantiate(_dataForLevel.PlayerCharacterPrefab, _playerSpawnPoint.position,
            _playerSpawnPoint.rotation);
        _cameraController.Initialize(_runCameraPoint, _playerView.BattleCameraPoint, _levelMover);
        _level = new(_cameraController, _levelMover, _playerView);
        DifficultCalculator difficultCalculator = new(_enemyPrefabs);
        difficultCalculator.Calculate(_playerStats.CurrentLevel);
        EnemyCreator enemyCreator = new(_level, _environment, difficultCalculator);
        LevelPartCreator levelPartCreator = new(_level, enemyCreator, _levelPartObjectPool, 
            _finalLevelPart, _cameraController, _playerStats, difficultCalculator, _finishPopup, _environment);
        PlayerCharacterInitialize();
        LevelEnd levelEnd = new(_playerView.Model, _sceneLoader);
        LevelUIInitializer levelUIInitializer = new(_toShopButtons, _sceneLoader);
        _levelMover.Initialize();

        foreach (var enemyTrigger in _enemyTriggers)
            enemyTrigger.Initialize(_level);

        levelPartCreator.Create(_firstLevelPartSpawnPoint.position);
    }

    private void PlayerCharacterInitialize()
    {
        _playerView.Initialize(_enemyTriggers, _level, _dataForLevel.PlayerDamage, 
            _dataForLevel.PlayerHealth, _dataForLevel.PlayerFiringRate);
        _playerView.RunDirectionPoint = _runDirectionPoint;
    }
}
