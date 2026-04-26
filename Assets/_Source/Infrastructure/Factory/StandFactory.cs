using System;
using Contracts.Signal;
using Contracts.Signal.LoadScene;
using View;
using Model;
using Presenter;
using Plugins.MessagePipe.MessageBus.Runtime;
using Zenject;

namespace Infrastructure.Factory
{
    public class StandFactory : IInitializable, IMessageDisposable
    {
        public event Action OnDispose;
        
        [Inject] private readonly MessageBus _messageBus;
        [Inject] private readonly StandView _standView;
        
        public void Initialize()
        {
            this.Subscribe(_messageBus, (CreateStandSignal signal) => CreateStand(signal.MaxAngle, signal.TargetFriction));
            this.Subscribe(_messageBus, (ClearSceneSignal signal) => Dispose());
        }

        private void CreateStand(int maxAngle, float targetFriction)
        {
            var presenter = new StandPresenter(_standView);
            var model = new StandModel(maxAngle, targetFriction, _messageBus, presenter);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}