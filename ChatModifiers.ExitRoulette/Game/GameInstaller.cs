using Zenject;

namespace ExitRoulette.Game
{
    internal class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameCoreHandler>().AsSingle();
        }
    }
}
