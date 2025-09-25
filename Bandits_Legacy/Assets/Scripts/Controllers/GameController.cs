using System.Collections.Generic;
using Core;
using Entitas;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using Systems;
using Systems.AI;
using Systems.Collectibles;
using Systems.Combat;
using Systems.GameCycle;
using Systems.Health;
using Systems.Input;
using Systems.Movement;
using Systems.UI;
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
        private Entitas.Systems _aiSystems;
        private Entitas.Systems _collectibleSystems;
        private Entitas.Systems _uiSystems;
        private PauseUnpauseSystem _pauseUnpauseSystem;
        
        private bool _isGameOver;
        private bool _onPause;
        
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
            _aiSystems = CreateAISystems(_contexts);
            _collectibleSystems = CreateCollectiblesSystems(_contexts);
            _uiSystems = CreateUISystems(_contexts);
            
            _gameSystems.Initialize();
            _inputSystems.Initialize();
            _eventSystems.Initialize();
            _visualSystems.Initialize();
            _combatSystems.Initialize();
            _aiSystems.Initialize();
            _collectibleSystems.Initialize();
            _uiSystems.Initialize();

            _pauseUnpauseSystem = new PauseUnpauseSystem(_contexts.gameCycle);
            _pauseUnpauseSystem.OnPauseRequested += OnPauseRequested;

            var spawnController = new SpawnController(_sceneScopeContainer);
            spawnController.SpawnUnits();
        }

        private void OnPauseRequested(bool shouldPause)
        {
            if(shouldPause)
                PauseGame();
            else
                UnpauseGame();
        }

        private void PauseGame()
        {
            _onPause = true;
            _gameSystems.DeactivateReactiveSystems();
            _eventSystems.DeactivateReactiveSystems();
            _visualSystems.DeactivateReactiveSystems();
            _combatSystems.DeactivateReactiveSystems();
            _aiSystems.DeactivateReactiveSystems();
            _collectibleSystems.DeactivateReactiveSystems();
            Time.timeScale = 0;
        }

        private void UnpauseGame()
        {
            _onPause = false;
            _gameSystems.ActivateReactiveSystems();
            _eventSystems.ActivateReactiveSystems();
            _visualSystems.ActivateReactiveSystems();
            _combatSystems.ActivateReactiveSystems();
            _aiSystems.ActivateReactiveSystems();
            _collectibleSystems.ActivateReactiveSystems();
            Time.timeScale = 1;
        }
        
        private void Update()
        {
            _inputSystems.Execute();
            _uiSystems.Execute();
            _pauseUnpauseSystem.Execute();

            if (_isGameOver || _onPause) 
                return;
            _gameSystems.Execute();
            _eventSystems.Execute();
            _visualSystems.Execute();
            _combatSystems.Execute();
            _aiSystems.Execute();
            _collectibleSystems.Execute();
            
        }

        private void OnDestroy()
        {
            _inputSystems.Cleanup();
            _gameSystems.Cleanup();
            _eventSystems.Cleanup();
            _visualSystems.Cleanup();
            _combatSystems.Cleanup();
            _aiSystems.Cleanup();
            _collectibleSystems.Cleanup();
            _uiSystems.Cleanup();
            _pauseUnpauseSystem.OnPauseRequested -= OnPauseRequested;
        }

        private Entitas.Systems CreateGameSystems(Contexts contexts)
        {
            return new Feature("Game Systems")
                .Add(new MovementSystem(contexts))
                .Add(new UnableToMoveSystem(contexts))
                .Add(new JumpSystem(contexts))
                .Add(new RenderDirectionSystem(contexts))
                .Add(new ReadGroundSensorSystem(contexts))
                .Add(new HealthSystem(contexts))
                .Add(new TakeDamageSystem(contexts))
                .Add(new HealDamageSystem(contexts))
                .Add(new RemoveDestroyedObjectsSystem(contexts));
        }
        
        private Entitas.Systems CreateInputSystems(Contexts contexts)
        {
            return new Feature("Input Systems")
                .Add(new EmitInputSystem())
                .Add(new ReadMoveInputSystem(contexts))
                .Add(new ReadJumpInputSystem(contexts))
                .Add(new ReadAttackInputSystem(contexts))
                .Add(new ReadOpenMenuSystem(contexts));
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
                .Add(new DeathAnimationSystem(contexts))
                .Add(new HitTakenAnimationSystem(contexts));
        }

        private Entitas.Systems CreateCombatSystems(Contexts contexts)
        {
            return new Feature("Combat systems")
                .Add(new AttackSystem(contexts))
                .Add(new AttackDelaySystem(contexts))
                .Add(new CheckCollisionsSystem(contexts));
        }

        private Entitas.Systems CreateAISystems(Contexts contexts)
        {
            return new Feature("AI Systems")
                .Add(new AIPatrolSystem(contexts))
                .Add(new AISightSystem(contexts))
                .Add(new AIReactToEnemySightedSystem(contexts))
                .Add(new AIIdleSystem(contexts))
                .Add(new AIChaseSystem(contexts))
                .Add(new AIAttackSystem(contexts))
                .Add(new AIPeaceSystem(contexts));
        }

        private Entitas.Systems CreateCollectiblesSystems(Contexts contexts)
        {
            return new Feature("Collectibles Systems")
                .Add(new CheckForDropSystem(contexts, _sceneScopeContainer))
                .Add(new CollectibleSpawnSystem(contexts, _sceneScopeContainer))
                .Add(new HandleCollectibleTriggeredSystem(contexts))
                .Add(new HealthCollectiblePickupSystem(contexts))
                .Add(new MoneyCollectiblePickupSystem(contexts, _sceneScopeContainer));
        }

        private Entitas.Systems CreateUISystems(Contexts contexts)
        {
            return new Feature("UI Systems")
                .Add(new PlayerHitPointsUISystem(contexts, _sceneScopeContainer))
                .Add(new OpenMenuSystem(contexts, _sceneScopeContainer));
        }
    }
}