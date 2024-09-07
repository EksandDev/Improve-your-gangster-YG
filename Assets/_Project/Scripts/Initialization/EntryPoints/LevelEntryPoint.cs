using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private Transform _environment;
    [SerializeField] private Transform _firstLevelPartSpawnPoint;
    [SerializeField] private EnemyTrigger[] _enemyTriggers;
    [SerializeField] private EnemyView[] _enemies;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHealth;

    private LevelMover _levelMover;
    private CameraController _cameraController;
    private PlayerCharacterView _playerView;
    private EnemyObjectPool _enemyObjectPool;
    private LevelPartObjectPool _levelPartObjectPool;

    #region Zenject initialization
    [Inject]
    private void Construct(LevelMover levelMover, CameraController cameraController, PlayerCharacterView playerView,
        EnemyObjectPool enemyObjectPool, LevelPartObjectPool levelPartObjectPool)
    {
        _levelMover = levelMover;
        _cameraController = cameraController;
        _playerView = playerView;
        _enemyObjectPool = enemyObjectPool;
        _levelPartObjectPool = levelPartObjectPool;
    }
    #endregion

    private void Awake()
    {
        _sceneContext.Run();

        Level level = new(_cameraController, _levelMover, _playerView);
        EnemyCreator enemyCreator = new(level, _enemyObjectPool, _environment);
        LevelPartCreator levelPartCreator = new(level, enemyCreator, _levelPartObjectPool, _environment);

        _playerView.Initialize(_enemyTriggers, level, _damage, _maxHealth);
        _levelMover.Initialize();

        foreach (var enemyTrigger in _enemyTriggers)
            enemyTrigger.Initialize(level);

        levelPartCreator.Create(_firstLevelPartSpawnPoint.position);
    }
}
