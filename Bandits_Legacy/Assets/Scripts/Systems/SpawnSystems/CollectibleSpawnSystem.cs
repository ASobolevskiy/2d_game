using System.Collections.Generic;
using Core;
using Entitas;
using Entitas.Unity;
using Reflex.Core;
using UnityEngine;
using Utils;

namespace Systems
{
    public class CollectibleSpawnSystem : ReactiveSystem<EventsEntity>
    {
        private readonly Contexts _contexts;
        private readonly Transform _worldTransform;
        
        public CollectibleSpawnSystem(Contexts contexts, Container sceneContainer) : base(contexts.events)
        {
            _contexts = contexts;
            _worldTransform = sceneContainer.Resolve<Transform>();
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            return context.CreateCollector(EventsMatcher.CollectibleSpawnRequested);
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.isCollectibleSpawnRequested &&
                   entity.hasCollectiblePrefab &&
                   entity.hasPosition;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            foreach (var entity in entities)
            {
                var prefab = entity.collectiblePrefab.Value;
                var gameEntity = _contexts.game.CreateEntity();
                gameEntity.AddPosition(entity.position.Value);
                gameEntity.AddCollectibleType(prefab.CollectibleType);
                gameEntity.AddCollectibleValue(prefab.Amount);
                gameEntity.isCollectible = true;
                
                var gameObject = Object.Instantiate(prefab.gameObject, gameEntity.position.Value, Quaternion.identity);
                gameObject.transform.SetParent(_worldTransform);
                gameObject.Link(gameEntity);
                gameEntity.AddSceneView(gameObject);
                
                if(gameEntity.sceneView.Value.TryGetComponent(out TriggerCollisionComponent trigger))
                    trigger.SetGameEntity(gameEntity);
                
                entity.Destroy();
            }
        }
    }
}