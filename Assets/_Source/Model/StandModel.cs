using System;
using Contracts.Signal;
using Contracts.Signal.LoadScene;
using Presenter;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Model
{
    public class StandModel : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly int _maxAngle;
        private readonly float _targetFriction;
        private readonly MessageBus _messageBus;
        private readonly StandPresenter _standPresenter;

        private int _angle;


        public StandModel(int maxAngle, float targetFriction, MessageBus messageBus, StandPresenter standPresenter)
        {
            _maxAngle = maxAngle;
            _targetFriction = targetFriction;
            _messageBus = messageBus;
            _standPresenter = standPresenter;

            this.Subscribe(messageBus, (AddAngleSignal signal) => AddAngle());
            this.Subscribe(messageBus, (ClearSceneSignal signal) => Dispose());
        }

        private void AddAngle()
        {
            if(_angle >= _maxAngle)
                return;
            
            _angle++;
            
            _messageBus.Publish(new OnAngleChangeSignal(_angle, _maxAngle));
            
            _standPresenter.SetAngle(_angle, CanStartMove());
        }

        private bool CanStartMove()
        {
            var radians = _angle * Math.PI / 180.0;
            float currentFriction = (float)Math.Tan(radians);
            
            if (currentFriction >= _targetFriction)
                return true;

            return false;
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}