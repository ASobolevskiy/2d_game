using Reflex.Core;
using UnityEngine;

namespace ReflexInstallers
{
    public sealed class SceneScopeInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private SpawnInstaller _spawnInstaller;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            _spawnInstaller.InstallBindings(containerBuilder);
        }
    }
}