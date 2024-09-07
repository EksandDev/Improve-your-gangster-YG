using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private EnemyTrigger[] _enemyTriggers;
    [SerializeField] private EnemyView[] _enemies;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private EnemyData _data;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHealth;

    private LevelMover _levelMover;
    private CameraController _cameraController;
    private PlayerCharacterView _playerView;

    #region Zenject initialization
    [Inject]
    private void Construct(LevelMover levelMover, CameraController cameraController, PlayerCharacterView playerView)
    {
        _levelMover = levelMover;
        _cameraController = cameraController;
        _playerView = playerView;
    }
    #endregion

    private void Start()
    {
        _sceneContext.Run();

        Level level = new(_cameraController, _levelMover, _playerView);
        EnemyCreator enemyCreator = new(_enemies, level);
        LevelPartCreator levelPartCreator = new(null, level, enemyCreator);

        foreach (var spawnPoint in _spawnPoints)
            enemyCreator.Create(spawnPoint.position, _levelMover.transform);

        _playerView.Initialize(_enemyTriggers, level, _damage, _maxHealth);
        _levelMover.Initialize();

        foreach (var enemyTrigger in _enemyTriggers)
            enemyTrigger.Initialize(level);
    }
}
