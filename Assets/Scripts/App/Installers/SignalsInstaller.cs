
using Assets.Scripts.Signals;
using ShootEmUp;
using Zenject;

internal class SignalsInstaller: MonoInstaller<SignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<CollisionEvent>();
        Container.DeclareSignal<FinishGameEvent>();
        Container.DeclareSignal<OnEnemyDieEvent>();
        Container.DeclareSignal<OnBulletDieEvent>();

        Container
            .BindSignal<FinishGameEvent>()
            .ToMethod<GameManager>(x => x.FinishGame)
            .FromResolve();
    }
}
