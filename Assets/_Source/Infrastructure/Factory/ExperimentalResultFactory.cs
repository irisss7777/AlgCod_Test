using System;
using Contracts.Signal;
using Contracts.Signal.LoadScene;
using Model;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presenter;
using View.UI;
using Zenject;

namespace Infrastructure.Factory
{
    public class ExperimentalResultFactory : IInitializable, IMessageDisposable
    {
        public event Action OnDispose;
        
        [Inject] private readonly MessageBus _messageBus;
        [Inject] private readonly ExperimentResultView _experimentResultView;
        
        public void Initialize()
        {
            this.Subscribe(_messageBus, (CreateExperimentalResultSignal signal) => CreateExperimentalResult(signal.FirstMaterial, signal.SecondMaterial));
            this.Subscribe(_messageBus, (ClearSceneSignal signal) => Dispose());
        }

        private void CreateExperimentalResult(int firstMaterial, int secondMaterial)
        {
            var presenter = new ExperimentalResultPresenter(_experimentResultView);
            var model = new ExperimentResultModel(firstMaterial, secondMaterial, _messageBus, presenter);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}