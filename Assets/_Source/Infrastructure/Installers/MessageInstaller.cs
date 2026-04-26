using Contracts.Signal;
using Contracts.Signal.LoadScene;
using MessagePipe;
using Plugins.MessagePipe.MessageBus.Runtime;
using Zenject;

namespace Infrastructure.Installers
{
    public class MessageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var options = Container.BindMessagePipe();

            Container.Bind<MessageBus>().AsSingle();

            LoadSceneBind(options);
            FactoryBind(options);
            StandBind(options);
        }
        
        private void LoadSceneBind(MessagePipeOptions options)
        {
            Container.BindMessageBroker<LoadMenuSignal>(options);
            Container.BindMessageBroker<ReloadSceneSignal>(options);
            Container.BindMessageBroker<ClearSceneSignal>(options);
            Container.BindMessageBroker<LoadLabSignal>(options);
        }
        
        private void FactoryBind(MessagePipeOptions options)
        {
            Container.BindMessageBroker<CreateStandSignal>(options);
            Container.BindMessageBroker<CreateExperimentalResultSignal>(options);
        }
        
        private void StandBind(MessagePipeOptions options)
        {
            Container.BindMessageBroker<AddAngleSignal>(options);
            Container.BindMessageBroker<OnAngleChangeSignal>(options);
            Container.BindMessageBroker<ExperimentResultSignal>(options);
        }
    }
}