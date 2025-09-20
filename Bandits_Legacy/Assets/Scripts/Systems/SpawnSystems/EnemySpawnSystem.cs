using System.Collections.Generic;
using Core;
using Entitas;
using Entitas.Unity;
using Reflex.Core;
using UnityEngine;
using Utils;

namespace Systems
{
    public sealed class EnemySpawnSystem : ReactiveSystem<EventsEntity>
    {
        private readonly Contexts _contexts;
        private readonly Transform _worldTransform;
        
        public EnemySpawnSystem(Contexts contexts, Container sceneContainer) : base(contexts.events)
        {
            _contexts = contexts;
            _worldTransform = sceneContainer.Resolve<Transform>();
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            var matches = new[]
            {
                EventsMatcher.EnemySpawnRequested
            };
            return context.CreateCollector(EventsMatcher.AllOf((matches)));
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.isEnemySpawnRequested &&
                   entity.hasPosition &&
                   entity.hasEnemyPrefab;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            foreach (var entity in entities)
            {
                var prefab = entity.enemyPrefab.Value;
                var gameEntity = _contexts.game.CreateEntity();
                
                gameEntity.AddPosition(entity.position.Value);
                gameEntity.AddDirection(Vector2.zero);
                gameEntity.AddSpeed(1);
                gameEntity.isAbleToMove = true;
                gameEntity.isMovable = true;
                gameEntity.AddFaceDirection(FaceDirectionEnum.Left);
                
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
                entity.Destroy();
            }
        }
    }
}