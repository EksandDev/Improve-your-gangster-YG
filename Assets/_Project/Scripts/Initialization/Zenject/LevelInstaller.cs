using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private LevelMover _levelMover;
    [SerializeField] private PlayerCharacterView _playerView;

    public override void InstallBindings()
    {
        Container.Bind<PlayerCharacterView>().FromInstance(_playerView).AsSingle();
        Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
        Container.Bind<LevelMover>().FromInstance(_levelMover).AsSingle();
    }
}