using System;
using Contracts.Signal;
using Cysharp.Threading.Tasks;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Controllers
{
    public class AddAngleController : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        
        [SerializeField] private Button _addAngleButton;

        private bool _canClick = true;

        private void Awake()
        {
            _addAngleButton.onClick.AddListener(OnAddAngle);
        }

        private void OnAddAngle()
        {
            if(!_canClick)
                return;
            
            _messageBus.Publish(new AddAngleSignal());
            
            ButtonDelay().Forget();
        }

        private async UniTask ButtonDelay()
        {
            _canClick = false;

            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            _canClick = true;
        }

        private void OnDestroy()
        {
            _addAngleButton.onClick.RemoveListener(OnAddAngle);
        }
    }
}