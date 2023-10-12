using Zenject;

// Zenject（プラグイン）を使い、IPlayerAreaMoveをPlayerAreaMoveに依存させている
public class PlayerAreaMoveInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IPlayerAreaMove>()
            .To<PlayerAreaMove>()
            .FromComponentInHierarchy()
            .AsCached();
    }
}