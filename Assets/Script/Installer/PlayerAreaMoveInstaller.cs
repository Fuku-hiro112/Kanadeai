using Zenject;

// Zenject�i�v���O�C���j���g���AIPlayerAreaMove��PlayerAreaMove�Ɉˑ������Ă���
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