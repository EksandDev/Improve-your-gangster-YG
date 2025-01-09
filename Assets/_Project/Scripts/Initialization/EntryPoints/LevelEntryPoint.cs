using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private Popup _failurePopup;
    [SerializeField] private Popup _finishPopup;
    [SerializeField] private Slider _playerHealthSlider; 
    [SerializeField] private LevelStatsCounterUI[] _levelStatsCountersUI;
    [SerializeField] private SceneLoaderButton[] _sceneLoaderButtons;

    [Header("Enemy things")]
    [SerializeField] private EnemyView[] _enemyPrefabs;
    [SerializeField] private EnemyTrigger[] _enemyTriggers;

    [Header("Particle object pools")]
    [SerializeField] private ParticleObjectPool _bloodParticleObjectPool;
    [SerializeField] private ParticleObjectPool _shotParticleObjectPool;

    [Header("Other")]
    [SerializeField] private FinalLevelPart _finalLevelPart;
    [SerializeField] private SceneContext _sceneContext;

    private Level _level;
    private LevelMover _levelMover;
    private CameraController _cameraController;
    private SceneLoader _sceneLoader;
    private PlayerCharacterView _playerView;
    private LevelPartObjectPool _levelPartObjectPool;
    private DataForLevel _dataForLevel;
    private PlayerStats _playerStats;
    private ParticleCreator _bloodParticleCreator;
    private ParticleCreator _shotParticleCreator;
    private ParticleController _particleController;

    #region Zenject initialization
    [Inject]
    private void Construct(LevelMover levelMover, CameraController cameraController, SceneLoader sceneLoader, 
        LevelPartObjectPool levelPartObjectPool, DataForLevel dataForLevel,PlayerStats playerStats)
    {
        _levelMover = levelMover;
        _cameraController = cameraController;
        _sceneLoader = sceneLoader;
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
        ParticleCreatorsInitialize();
        DifficultCalculator difficultCalculator = new(_enemyPrefabs);
        difficultCalculator.Calculate(_playerStats.CurrentLevel);
        LevelStatsCounter levelStatsCounter = new();
        EnemyCreator enemyCreator = new(_level, _environment, difficultCalculator, levelStatsCounter, 
            _particleController);
        PlayerCharacterInitialize();
        LevelEnd levelEnd = new(_playerView.Model, _cameraController, _playerStats, _finishPopup, _failurePopup);
        LevelPartCreator levelPartCreator = new(_level, enemyCreator, _levelPartObjectPool, 
            _finalLevelPart, difficultCalculator, levelEnd, _environment);
        LevelUIInitializer levelUIInitializer = new(_sceneLoaderButtons, _levelStatsCountersUI,
            _sceneLoader, levelStatsCounter, _playerStats);
        _levelMover.Initialize();

        foreach (var enemyTrigger in _enemyTriggers)
            enemyTrigger.Initialize(_level);

        levelPartCreator.Create(_firstLevelPartSpawnPoint.position);
    }

    private void PlayerCharacterInitialize()
    {
        _playerView.Initialize(_enemyTriggers, _level, _dataForLevel.PlayerDamage, _dataForLevel.PlayerHealth, 
            _dataForLevel.PlayerFiringRate, _playerHealthSlider, _particleController);
        _playerView.RunDirectionPoint = _runDirectionPoint;
    }

    private void ParticleCreatorsInitialize()
    {
        _bloodParticleCreator = new(_bloodParticleObjectPool, _levelMover);
        _shotParticleCreator = new(_shotParticleObjectPool, _levelMover);
        _particleController = new(_bloodParticleCreator, _shotParticleCreator);
    }
}
