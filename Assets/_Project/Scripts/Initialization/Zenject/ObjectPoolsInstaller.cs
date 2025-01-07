using UnityEngine;
using Zenject;

public class ObjectPoolsInstaller : MonoInstaller
{
    [SerializeField] private LevelPartObjectPool _levelPartObjectPool;

    public override void InstallBindings()
    {
        Container.Bind<LevelPartObjectPool>().FromInstance(_levelPartObjectPool).AsSingle();
    }
}
