
using ShootEmUp;
using UnityEngine;
using Zenject;

internal class GameSysInstaller: MonoInstaller<GameSysInstaller>
{
    [SerializeField] private AsyncProccesor asyncProccesor;
    public override void InstallBindings()
    {
        var asynProc = Container.InstantiatePrefabForComponent<AsyncProccesor>(asyncProccesor);
        Container.Bind<AsyncProccesor>()
            .FromInstance(asynProc)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<CollisionSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        Container.Bind<GameManager>().AsSingle();
    }
}
