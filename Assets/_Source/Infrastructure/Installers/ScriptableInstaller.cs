using Infrastructure.Database;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Installers/ScriptableInstaller")]
    public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
    {
        [SerializeField] private MaterialDatabase _materialDatabase;
        
        public override void InstallBindings()
        {
            Container.Bind<MaterialDatabase>().FromInstance(_materialDatabase).AsSingle();
        }
    }
}