using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using Systems;
using Systems.Input;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        private Contexts _contexts;
        private Container _sceneScopeContainer;
        private Entitas.Systems _gameSystems;
        private Entitas.Systems _eventSystems;
        private Entitas.Systems _inputSystems;
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
            _inputSystems = CreateInputSystems(_contexts);
            _eventSystems = CreateEventsSystems(_contexts);
            
            _gameSystems.Initialize();
            _inputSystems.Initialize();
            _eventSystems.Initialize();

            var enemiesPrefabs = _sceneScopeContainer.Resolve<List<Enemy>>();
            //TODO move to spawner 
            if (_spawnPoints != null)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    var position = spawnPoint.Coordinates.position;
                    var eventEntity = _contexts.events.CreateEntity();
                    eventEntity.AddPosition(position);
                    
                    if (spawnPoint.IsPlayerSpawnPoint)
                    {
                        //Spawn player
                        eventEntity.isPlayerSpawnRequested = true;
                        eventEntity.AddPlayerPrefab(_sceneScopeContainer.Resolve<Player>());
                    }
                    else
                    {
                        //Spawn enemies
                        eventEntity.isEnemySpawnRequested = true;
                        eventEntity.AddEnemyPrefab(GetRandomEnemyPrefab(enemiesPrefabs));
                    }
                }
            }
        }

        private Enemy GetRandomEnemyPrefab(List<Enemy> prefabs)
        {
            var index = Random.Range(0, prefabs.Count - 1);
            return prefabs[index];
        }

        private void Update()
        {
            if (_isGameOver)
                return;
            _inputSystems.Execute();
            _gameSystems.Execute();
            _gameSystems.Cleanup();
            
            _eventSystems.Execute();
            _eventSystems.Cleanup();
        }

        private Entitas.Systems CreateGameSystems(Contexts contexts)
        {
            return new Feature("Game Systems")
                .Add(new MovementSystem(contexts))
                .Add(new RenderDirectionSystem(contexts));
        }
        
        private Entitas.Systems CreateInputSystems(Contexts contexts)
        {
            return new Feature("Input Systems")
                .Add(new EmitInputSystem());
        }
        
        private Entitas.Systems CreateEventsSystems(Contexts contexts)
        {
            var playerSpawnSystem = new PlayerSpawnSystem(contexts);
            AttributeInjector.Inject(playerSpawnSystem, _sceneScopeContainer);
            return new Feature("Events Systems")
                .Add(playerSpawnSystem)
                .Add(new EnemySpawnSystem(contexts, _sceneScopeContainer));
        }
    }
}