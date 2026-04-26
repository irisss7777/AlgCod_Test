using System;
using Contracts.Signal.LoadScene;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine.SceneManagement;
using Zenject;

namespace Utils
{
    public class LoadSceneService : IInitializable, IMessageDisposable
    {
        public event Action OnDispose;

        [Inject] private readonly MessageBus _messageBus;

        public void Initialize()
        {
            this.Subscribe(_messageBus, (LoadLabSignal signal) => LoadGameScene(signal.LabId));
            this.Subscribe(_messageBus, (LoadMenuSignal signal) => LoadMenu());
            this.Subscribe(_messageBus, (ReloadSceneSignal signal) => ReloadScene());
        }

        private void LoadGameScene(int id)
        {
            string sceneName = "Lab" + id;
            SceneManager.LoadScene(sceneName);
        }

        private void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        
        private void ReloadScene()
        {
            var scene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(scene.buildIndex);
            
            _messageBus.Publish(new ClearSceneSignal());
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}