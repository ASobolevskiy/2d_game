using System;
using System.Collections.Generic;
using Core;
using Reflex.Core;
using UnityEngine;

namespace ReflexInstallers
{
    [Serializable]
    public class SpawnInstaller : IInstaller
    {
        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private List<SpawnPoint> _spawnPoints;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_worldTransform);
            containerBuilder.AddSingleton(_spawnPoints);
        }
    }
}