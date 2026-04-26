using System.Text;
using Contracts.Signal;
using Contracts.Signal.LoadScene;
using Infrastructure.Database;
using Infrastructure.Factory;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using View.Controllers;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        [Inject] private readonly MaterialSelectionFactory _materialSelectionFactory;
        [Inject] private readonly ChoosePlatformsController _choosePlatformsController;
        [Inject] private readonly MaterialDatabase _materialDatabase;

        private void Start()
        {
            _materialDatabase.LoadFromResources();
            
            _choosePlatformsController.OnSelected += SetupStand;
            _materialSelectionFactory.CreateMaterialSelection();
        }

        private void SetupStand(int firstMaterial, int secondMaterial)
        {
            _messageBus.Publish(new CreateStandSignal(_materialDatabase.MaxAngle, _materialDatabase.GetFriction(firstMaterial, secondMaterial)));

            var materials = _materialDatabase.GetMaterial();

            materials.Item1++;
            materials.Item2++;
            
            _messageBus.Publish(new CreateExperimentalResultSignal(materials.Item1, materials.Item2));
        }

        private void OnDestroy()
        {
            _messageBus.Publish(new ClearSceneSignal());
            _choosePlatformsController.OnSelected -= SetupStand;
        }
    }
}