using UnityEngine;
using Zenject;

public class ObjectPoolsInstaller : MonoInstaller
{
    [SerializeField] private LevelPartObjectPool _levelPartObjectPool;
    [SerializeField] private EnemyObjectPool _enemyObjectPool;

    public override void InstallBindings()
    {
        Container.Bind<LevelPartObjectPool>().FromInstance(_levelPartObjectPool).AsSingle();
        Container.Bind<EnemyObjectPool>().FromInstance(_enemyObjectPool).AsSingle();
    }
}
