using System.Collections.Generic;
using Core;
using Entitas;
using Entitas.Unity;
using Reflex.Attributes;
using UnityEngine;
using Utils;

namespace Systems
{
    public sealed class PlayerSpawnSystem : ReactiveSystem<EventsEntity>
    {
        [Inject]
        private Transform _worldTransform;

        private readonly Contexts _contexts;
        
        public PlayerSpawnSystem(Contexts contexts) : base(contexts.events)
        {
            _contexts = contexts;
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            var matches = new[]
            {
                EventsMatcher.PlayerSpawnRequested
            };
            return context.CreateCollector(EventsMatcher.AllOf((matches)));
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.isPlayerSpawnRequested &&
                   entity.hasPosition &&
                   entity.hasPlayerPrefab;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            foreach (var entity in entities)
            {
                var prefab = entity.playerPrefab.Value;
                var gameEntity = _contexts.game.CreateEntity();
                
                gameEntity.AddPosition(entity.position.Value);
                gameEntity.AddDirection(Vector2.zero);
                gameEntity.AddSpeed(1);
                gameEntity.isAbleToMove = true;
                gameEntity.isMovable = true;
                gameEntity.isPlayer = true;
                gameEntity.AddFaceDirection(FaceDirectionEnum.Right);
                gameEntity.AddJumpForce(4);
                gameEntity.isAbleToJump = true;

                var gameObject = Object.Instantiate(prefab.gameObject, gameEntity.position.Value, Quaternion.identity);
                gameObject.transform.SetParent(_worldTransform);
                gameObject.Link(gameEntity);
                gameEntity.AddSceneView(gameObject);
                if (gameEntity.sceneView.Value.TryGetComponent(out Rigidbody2D rb))
                {
                    gameEntity.AddRigidBody2D(rb);
                }

                if (gameEntity.sceneView.Value.transform.Find("GroundSensor").TryGetComponent(out GroundSensor sensor))
                {
                    gameEntity.AddGroundSensor(sensor);
                }

                if (gameEntity.sceneView.Value.transform.Find("Visual").TryGetComponent(out Animator animator))
                {
                    gameEntity.AddAnimator(animator);
                }

                if (gameEntity.sceneView.Value.transform.Find("AttackPoint").TryGetComponent(out Transform transform))
                {
                    gameEntity.AddWeapon(transform, 1, 2f);
                }
                
                if (gameEntity.sceneView.Value.transform.Find("Visual").TryGetComponent(out AnimationEventHandler animHandler))
                {
                    animHandler.SetGameEntity(gameEntity);
                }

                gameEntity.isAttackDelaying = false;
                gameEntity.AddHitPoints(10);
                gameEntity.isDead = false;
                entity.Destroy();
            }   
        }
    }
}