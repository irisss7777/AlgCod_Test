using System;
using Contracts.Signal;
using Contracts.Signal.LoadScene;
using Cysharp.Threading.Tasks;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presenter;

namespace Model
{
    public class ExperimentResultModel : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly MessageBus _messageBus;
        private readonly ExperimentalResultPresenter _presenter;

        private readonly int _firstMaterial;
        private readonly int _secondMaterial;


        public ExperimentResultModel(int firstMaterial, int secondMaterial, MessageBus messageBus, ExperimentalResultPresenter presenter)
        {
            _firstMaterial = firstMaterial;
            _secondMaterial = secondMaterial;
            _messageBus = messageBus;
            _presenter = presenter;
            
            this.Subscribe(_messageBus, (ExperimentResultSignal signal) => GetExperimentResult(signal.FirstMaterial, signal.SecondMaterial));
        }

        private async UniTask GetExperimentResult(int firstMaterial, int secondMaterial)
        {
            bool experimentSuccess = (firstMaterial == _firstMaterial && secondMaterial == _secondMaterial) ||
                                     (firstMaterial == _secondMaterial && secondMaterial == _firstMaterial);
            
            _presenter.SetResult(experimentSuccess);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            _messageBus.Publish(new ReloadSceneSignal());
            Dispose();
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}