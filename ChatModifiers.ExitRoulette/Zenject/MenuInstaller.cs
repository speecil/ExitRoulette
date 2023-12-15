using Zenject;

namespace ExitRoulette.Zenject
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Menu.ViewController>().FromNewComponentAsViewController().AsSingle();
        }
    }
}
