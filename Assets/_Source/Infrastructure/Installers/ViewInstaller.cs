using View;
using UnityEngine;
using View.Controllers;
using View.UI;
using Zenject;

namespace Infrastructure.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private StandView _standView;
        [SerializeField] private ExperimentResultView _experimentResultView;
        [SerializeField] private ChoosePlatformsController _choosePlatformsController;
        
        public override void InstallBindings()
        {
            Container.Bind<StandView>().FromInstance(_standView).AsSingle();
            Container.Bind<ExperimentResultView>().FromInstance(_experimentResultView).AsSingle();
            Container.Bind<ChoosePlatformsController>().FromInstance(_choosePlatformsController).AsSingle();
        }
    }
}