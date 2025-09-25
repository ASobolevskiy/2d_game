using Controllers;
using Reflex.Core;
using UnityEngine;

namespace ReflexInstallers
{
    public sealed class SceneScopeInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private PopupController _popupController;
        
        [SerializeField]
        private SpawnInstaller _spawnInstaller;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_popupController);
            _spawnInstaller.InstallBindings(containerBuilder);
        }
    }
}