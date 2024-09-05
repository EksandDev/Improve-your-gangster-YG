using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private EnemyTrigger _enemyTrigger;
    [SerializeField] private EnemyView _enemyView;
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
        _playerView.Initialize(level, _damage, _maxHealth);
        _enemyTrigger.Initialize(level);
        _levelMover.Initialize();
        _enemyView.Initialize(true, level, _data.Damage, _data.MaxHealth);
    }
}
