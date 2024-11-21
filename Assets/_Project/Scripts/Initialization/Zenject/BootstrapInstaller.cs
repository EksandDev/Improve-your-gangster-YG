using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle();
        Container.Bind<DataForLevel>().AsSingle();
        Container.Bind<PlayerStats>().AsSingle();
        Container.Bind<NeedLoadTracker>().AsSingle();
    }
}
