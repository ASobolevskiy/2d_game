using System.Collections.Generic;
using Core;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using Systems;
using Systems.Combat;
using Systems.Health;
using Systems.Input;
using Systems.Movement;
using Systems.Utility;
using Systems.Visual.Animation;
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
        private Entitas.Systems _visualSystems;
        private Entitas.Systems _combatSystems;
        
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
            _visualSystems = CreateVisualSystems(_contexts);
            _combatSystems = CreateCombatSystems(_contexts);
            
            _gameSystems.Initialize();
            _inputSystems.Initialize();
            _eventSystems.Initialize();
            _visualSystems.Initialize();
            _combatSystems.Initialize();

            var enemiesPrefabs = _sceneScopeContainer.Resolve<List<Enemy>>();
            //TODO move to spawner 
            if (_spawnPoints == null) 
                return;
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
            _eventSystems.Execute();
            _visualSystems.Execute();
            _combatSystems.Execute();
        }

        private void OnDestroy()
        {
            _inputSystems.Cleanup();
            _gameSystems.Cleanup();
            _eventSystems.Cleanup();
            _visualSystems.Cleanup();
            _combatSystems.Cleanup();
        }

        private Entitas.Systems CreateGameSystems(Contexts contexts)
        {
            return new Feature("Game Systems")
                .Add(new MovementSystem(contexts))
                .Add(new JumpSystem(contexts))
                .Add(new RenderDirectionSystem(contexts))
                .Add(new ReadGroundSensorSystem(contexts))
                .Add(new HealthSystem(contexts));
        }
        
        private Entitas.Systems CreateInputSystems(Contexts contexts)
        {
            return new Feature("Input Systems")
                .Add(new EmitInputSystem())
                .Add(new ReadMoveInputSystem(contexts))
                .Add(new ReadJumpInputSystem(contexts))
                .Add(new ReadAttackInputSystem(contexts));
        }
        
        private Entitas.Systems CreateEventsSystems(Contexts contexts)
        {
            var playerSpawnSystem = new PlayerSpawnSystem(contexts);
            AttributeInjector.Inject(playerSpawnSystem, _sceneScopeContainer);
            return new Feature("Events Systems")
                .Add(playerSpawnSystem)
                .Add(new EnemySpawnSystem(contexts, _sceneScopeContainer));
        }

        private Entitas.Systems CreateVisualSystems(Contexts contexts)
        {
            return new Feature("Visual systems")
                .Add(new MovingAnimationSystem(contexts))
                .Add(new JumpAnimationSystem(contexts))
                .Add(new AttackAnimationSystem(contexts))
                .Add(new DeathAnimationSystem(contexts));
        }

        private Entitas.Systems CreateCombatSystems(Contexts contexts)
        {
            return new Feature("Combat systems")
                .Add(new AttackSystem(contexts))
                .Add(new AttackDelaySystem(contexts));
        }
    }
}