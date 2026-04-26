using System;
using Contracts.Signal;
using Contracts.Signal.LoadScene;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.UI
{
    public class AngleBarView : MonoBehaviour, IMessageDisposable
    {
        public event Action OnDispose;
        
        [Inject] private MessageBus _messageBus;

        [SerializeField] private Image _barImage;
        
        private void Awake()
        {
            this.Subscribe(_messageBus, (OnAngleChangeSignal signal) => SetBar(signal.Angle, signal.MaxAngle));
            this.Subscribe(_messageBus, (ClearSceneSignal signal) => Dispose());
        }

        private void SetBar(int angle, int maxAngle)
        {
            _barImage.fillAmount = (float)angle / maxAngle;
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}