using UnityEngine;
using Zenject;

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