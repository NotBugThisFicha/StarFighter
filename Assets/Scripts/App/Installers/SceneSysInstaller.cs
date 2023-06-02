
using ShootEmUp;
using Zenject;
using UnityEngine;

public class SceneSysInstaller: MonoInstaller<SceneSysInstaller>
{
    [SerializeField] private LevelBounds levelBounds;
    [SerializeField] private EnemyConfigs enemyConfigs;
    [SerializeField] private TransformContainers transfContainers;
    [SerializeField] private float enemySpawnRate;

    [SerializeField] private ShootEmUp.CharacterController characterController;
    public override void InstallBindings()
    {
        Container.Bind<LevelBounds>()
            .FromInstance(levelBounds)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<BulletSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().WithArguments(enemyConfigs, enemySpawnRate);
        Container.BindInterfacesAndSelfTo<BulletTracker>().AsSingle();
        Container.BindInstance(transfContainers).AsSingle();

        Container
            .Bind<ShootEmUp.CharacterController>()
            .FromInstance(characterController)
            .AsSingle();
    }
}
