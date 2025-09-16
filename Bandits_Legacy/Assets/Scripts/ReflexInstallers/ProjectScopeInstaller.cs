using Core;
using Reflex.Core;
using UnityEngine;

namespace ReflexInstallers
{
    public class ProjectScopeInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private Player _playerPrefab;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_playerPrefab);
        }
    }
}