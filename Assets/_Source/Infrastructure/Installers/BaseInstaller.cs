using Infrastructure.Factory;
using Zenject;

namespace Infrastructure.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StandFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ExperimentalResultFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MaterialSelectionFactory>().AsSingle().NonLazy();
        }
    }
}