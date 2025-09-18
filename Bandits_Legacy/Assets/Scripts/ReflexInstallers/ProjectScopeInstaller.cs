using System.Collections.Generic;
using Core;
using Reflex.Core;
using UnityEngine;

namespace ReflexInstallers
{
    public sealed class ProjectScopeInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private Player _playerPrefab;

        [SerializeField]
        private List<Enemy> _enemyPrefabsList;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_playerPrefab);
            containerBuilder.AddSingleton(_enemyPrefabsList);
        }
    }
}