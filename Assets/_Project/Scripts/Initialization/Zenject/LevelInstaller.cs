using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private LevelMover _levelMover;

    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
        Container.Bind<LevelMover>().FromInstance(_levelMover).AsSingle();
    }
}