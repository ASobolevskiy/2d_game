using System;
using System.Collections.Generic;
using Core;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using Systems;
using UnityEngine;

namespace Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        private Contexts _contexts;
        private Container _sceneScopeContainer;
        private Entitas.Systems _gameSystems;
        private Entitas.Systems _eventSystems;
        private bool _isGameOver;

        [Inject]
        private List<SpawnPoint> _spawnPoints;
        
        private void Start()
        {
            _contexts = Contexts.sharedInstance;
            _sceneScopeContainer = gameObject.scene.GetSceneContainer();
            
            //TODO get save/load system and check for saved data. If it exist - load, else default
            //register systems
            _gameSystems = CreateGameSystems(_contexts);
            _eventSystems = CreateEventsSystems(_contexts);
            
            _gameSystems.Initialize();
            _eventSystems.Initialize();
            
            //Spawn player
            //TODO move to spawner 
            if (_spawnPoints != null)
            {
                var spawnPoint = _spawnPoints[0]; //right now i have only player spawn point
                var position = spawnPoint.Coordinates.position;
                var eventEntity = _contexts.events.CreateEntity();
                eventEntity.isPlayerSpawnRequested = true;
                eventEntity.AddPosition(position);
                eventEntity.AddPlayerPrefab(_sceneScopeContainer.Resolve<Player>());
            }
            //Spawn enemies
        }

        private void Update()
        {
            if (_isGameOver)
                return;
            _gameSystems.Execute();
            _gameSystems.Cleanup();
            
            _eventSystems.Execute();
            _eventSystems.Cleanup();
        }

        private Entitas.Systems CreateGameSystems(Contexts contexts)
        {
            return new Feature("Game Systems");
        }
        
        private Entitas.Systems CreateEventsSystems(Contexts contexts)
        {
            var playerSpawnSystem = new PlayerSpawnSystem(contexts);
            AttributeInjector.Inject(playerSpawnSystem, _sceneScopeContainer);
            return new Feature("Events Systems")
                .Add(playerSpawnSystem);
        }
    }
}