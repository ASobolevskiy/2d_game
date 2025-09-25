using System.Collections.Generic;
using Controllers;
using Core;
using Reflex.Core;
using UI.Presenters;
using UnityEngine;
using Utils.Randomizers;

namespace ReflexInstallers
{
    public sealed class ProjectScopeInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private Player _playerPrefab;

        [SerializeField]
        private List<Enemy> _enemyPrefabsList;

        [SerializeField]
        private List<Collectible> _collectiblePrefabsList;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_playerPrefab);
            containerBuilder.AddSingleton(_enemyPrefabsList);
            containerBuilder.AddSingleton(new CollectibleDropRandomizer(_collectiblePrefabsList));
            containerBuilder.AddSingleton(new MoneyStorage());
            containerBuilder.AddSingleton(typeof(MoneyPresenter), typeof(IMoneyPresenter));
            containerBuilder.AddSingleton(typeof(HitPointsPresenter), typeof(IHitPointsPresenter));
        }
    }
}