using System;
using Contracts.Signal.LoadScene;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.UI
{
    public class LaboratoryUiView : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        
        [SerializeField] private Button _menuButton;

        private void Awake()
        {
            _menuButton.onClick.AddListener(GoToMenu);
        }

        private void GoToMenu()
        {
            _messageBus.Publish(new LoadMenuSignal());
        }

        private void OnDestroy()
        {
            _menuButton.onClick.RemoveListener(GoToMenu);
        }
    }
}